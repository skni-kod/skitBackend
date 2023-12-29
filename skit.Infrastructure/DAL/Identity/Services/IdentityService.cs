using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using skit.Core.Common.Services;
using skit.Core.Companies.Entities;
using skit.Core.Companies.Exceptions;
using skit.Core.Identity.DTO;
using skit.Core.Identity.Entities;
using skit.Core.Identity.Exceptions;
using skit.Core.Identity.Services;
using skit.Core.Identity.Static;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Identity.Services;

public sealed class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly EFContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateService _dateService;

    public IdentityService(UserManager<User> userManager, EFContext context, SignInManager<User> signInManager, ITokenService tokenService, ICurrentUserService currentUserService, IDateService dateService)
    {
        _userManager = userManager;
        _context = context;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _currentUserService = currentUserService;
        _dateService = dateService;
    }
    
    public async Task<Guid> SignUp(string email, string password, CancellationToken cancellationToken)
    {
        var userEmailIsNotUnique = await _userManager.Users.AnyAsync(x => x.Email == email, cancellationToken);
        
        if (userEmailIsNotUnique)
            throw new UserWithEmailExistsException();
        
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        
        var user = new User
        {
            Email = email,
            UserName = email
        };
        
        var createUser = await _userManager.CreateAsync(user, password);
        
        if (!createUser.Succeeded)
            throw new CreateUserException(createUser.Errors);

        var addEmailClaimResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
        if (!addEmailClaimResult.Succeeded)
            throw new AddClaimException();
        
        var addNameClaimResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        if (!addNameClaimResult.Succeeded)
            throw new AddClaimException();

        await transaction.CommitAsync(cancellationToken);

        return user.Id;
    }

    public async Task<JsonWebToken> SignIn(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email != null && x.Email.Equals(email),
                       cancellationToken)
                   ?? throw new InvalidCredentialsException();

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);

        if (!result.Succeeded)
            throw new SignInException(result);

        var jwt = await GenerateJwtAsync(user, cancellationToken);

        return jwt;
    }
    
    public async Task SignOut(string? refreshToken, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
                       .Include(x => x.RefreshTokens)
                       .SingleOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken)
                   ?? throw new UserNotFoundException();

        var token = user.RefreshTokens.FirstOrDefault(x => x.Token == refreshToken);
        user.DeleteRefreshToken(token);
        _context.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        await _signInManager.SignOutAsync();
    }

    public async Task<JsonWebToken> RefreshToken(string? refreshToken, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
                       .Include(x => x.RefreshTokens)
                       .SingleOrDefaultAsync(x => x.RefreshTokens.Any(t => t.Token == refreshToken), cancellationToken)
                   ?? throw new InvalidRefreshTokenException();

        var currentRefreshToken = user.RefreshTokens.Single(x => x.Token == refreshToken);

        if (currentRefreshToken.IsExpired)
            throw new InvalidRefreshTokenException();

        user.DeleteRefreshToken(currentRefreshToken);
        var jwt = await GenerateJwtAsync(user, cancellationToken);
        
        return jwt;
    }

    public async Task<ResetPasswordTokenDto> GeneratePasswordResetTokenAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.AsNoTracking()
                       .Where(x => x.Email == email)
                       .FirstOrDefaultAsync(cancellationToken)
                   ?? throw new UserNotFoundException();

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        return new ResetPasswordTokenDto
        {
            Token = token,
            UserId = user.Id
        };
    }

    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(User user, CancellationToken cancellationToken)
    {
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task ConfirmAccountAsync(Guid userId, string token, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken)
                   ?? throw new ConfirmAccountException();

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
            throw new ConfirmAccountException();
    }

    public async Task ResetPasswordAsync(Guid userId, string token, string password, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken)
                   ?? throw new ConfirmAccountException();
        
        var result = await _userManager.ResetPasswordAsync(user, token, password);
        if (!result.Succeeded)
            throw new ChangePasswordException(result.Errors);
    }

    public AuthenticationProperties? ConfigureGoogleAuthentication(string redirectUrl)
    {
        return _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
    }

    public async Task<JsonWebToken> GoogleAuthAsync(CancellationToken cancellationToken)
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
            throw new GoogleAuthFailedException();

        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
        JsonWebToken token;
        
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        if (result.Succeeded)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == info.Principal.FindFirst(ClaimTypes.Email).Value, cancellationToken)
                             ?? throw new GoogleAuthFailedException();

            token = await GenerateJwtAsync(user, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            
            return token;
        }

        var createdUser = new User
        {
            Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
            UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
            EmailConfirmed = true
        };
        
        var createResult = await _userManager.CreateAsync(createdUser);
        if (!createResult.Succeeded) 
            throw new GoogleAuthFailedException();
        
        var addEmailClaimResult = await _userManager.AddClaimAsync(createdUser, new Claim(ClaimTypes.Email, createdUser.Email));
        if (!addEmailClaimResult.Succeeded)
            throw new AddClaimException();
        
        var addNameClaimResult = await _userManager.AddClaimAsync(createdUser, new Claim(ClaimTypes.NameIdentifier, createdUser.Id.ToString()));
        if (!addNameClaimResult.Succeeded)
            throw new AddClaimException();
        
        var addLoginResult = await _userManager.AddLoginAsync(createdUser, info);
        if (!addLoginResult.Succeeded)
            throw new GoogleAuthFailedException();
        
        await _signInManager.RefreshSignInAsync(createdUser);
        
        token = await GenerateJwtAsync(createdUser, cancellationToken);
        
        await transaction.CommitAsync(cancellationToken);
        
        return token;
    }

    public async Task<JsonWebToken> AddCompanyToUserAsync(Guid userId, Guid companyId, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken)
                   ?? throw new UserNotFoundException();

        user.CompanyId = companyId;
        _context.Update(user);
        
        var addCompanyClaimResult = await _userManager.AddClaimAsync(user, new Claim(UserClaims.CompanyId, companyId.ToString()));
        if (!addCompanyClaimResult.Succeeded)
            throw new AddClaimException();
        
        var addRoleResult = await _userManager.AddToRoleAsync(user, UserRoles.CompanyOwner);
        if (!addRoleResult.Succeeded)
            throw new AddToRoleException();

        await _context.SaveChangesAsync(cancellationToken);

        var jwt = await GenerateJwtAsync(user, cancellationToken);

        return jwt;
    }

    private async Task<JsonWebToken> GenerateJwtAsync(User user, CancellationToken cancellationToken)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
        var userClaims = await _userManager.GetClaimsAsync(user);
            
        var jwt = await _tokenService.GenerateAccessTokenAsync(user.Id, user.Email, userRoles, userClaims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        jwt.RefreshToken = refreshToken;
        DeleteExpiredRefreshTokens(user);
        user.AddRefreshToken(refreshToken);
        
        _context.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        return jwt;
    }

    private void DeleteExpiredRefreshTokens(User user)
    {
        var expiredTokens = user.RefreshTokens.Where(token => token.IsExpired).ToList();
        foreach (var token in expiredTokens)
        {
            if(token.IsExpired)
                user.DeleteRefreshToken(token);
        }
    }
}