using System.Security.Claims;
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
    
    public async Task<Guid> SignUpCompany(string email, string companyName, string password, CancellationToken cancellationToken)
    {
        var userEmailIsNotUnique = await _userManager.Users.AnyAsync(x => x.Email == email, cancellationToken);
        
        if (userEmailIsNotUnique)
            throw new UserWithEmailExistsException();
        
        var companyNameIsNotUnique = await _context.Companies.AnyAsync(x => x.Name.Equals(companyName), cancellationToken);
        
        if (companyNameIsNotUnique)
            throw new CompanyWithNameExistsException();

        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        
        var user = new User
        {
            Email = email,
            UserName = email
        };
        
        var createUser = await _userManager.CreateAsync(user, password);
        
        if (!createUser.Succeeded)
            throw new CreateUserException(createUser.Errors);
        
        var company = Company.Create(companyName, user.Id);
        var companyResult = await _context.AddAsync(company, cancellationToken);
        user.CompanyId = companyResult.Entity.Id;
        _context.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        
        var addRoleResult = await _userManager.AddToRoleAsync(user, UserRoles.CompanyOwner);
        if (!addRoleResult.Succeeded)
            throw new AddToRoleException();
        
        var addEmailClaimResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
        if (!addEmailClaimResult.Succeeded)
            throw new AddClaimException();
        
        var addNameClaimResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        if (!addNameClaimResult.Succeeded)
            throw new AddClaimException();
        
        var addCompanyIdClaimResult = await _userManager.AddClaimAsync(user, new Claim(UserClaims.CompanyId, companyResult.Entity.Id.ToString()));
        if (!addCompanyIdClaimResult.Succeeded)
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

        var roles = await _userManager.GetRolesAsync(user);
        var claims = await _userManager.GetClaimsAsync(user);

        var jwt = await _tokenService.GenerateAccessToken(user.Id, user.Email!, roles, claims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        jwt.RefreshToken = refreshToken;
        
        DeleteExpiredRefreshTokens(user);
        user.AddRefreshToken(refreshToken);
        _context.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

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

        var roles = await _userManager.GetRolesAsync(user);
        var claims = await _userManager.GetClaimsAsync(user);

        var jwt = await _tokenService.GenerateAccessToken(user.Id, user.Email!, roles, claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        jwt.RefreshToken = newRefreshToken;
        
        user.DeleteRefreshToken(currentRefreshToken);
        user.AddRefreshToken(newRefreshToken);
        _context.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        
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

    public async Task<JsonWebToken> SignInGoogle(CancellationToken cancellationToken)
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
            throw new GoogleAuthFailedException();

        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
        JsonWebToken token;
        RefreshToken refreshToken;
        
        if (result.Succeeded)
        {
            var userResult = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == info.Principal.FindFirst(ClaimTypes.Email).Value, cancellationToken)
                             ?? throw new GoogleAuthFailedException();
            var userRoles = await _userManager.GetRolesAsync(userResult);
            
            token = await _tokenService.GenerateGoogleAccessToken(info.Principal, userResult.Id, userResult.Email, userRoles);
            refreshToken = _tokenService.GenerateRefreshToken();

            token.RefreshToken = refreshToken;
            DeleteExpiredRefreshTokens(userResult);
            userResult.AddRefreshToken(refreshToken);
            
            _context.Update(userResult);
            await _context.SaveChangesAsync(cancellationToken);
            
            return token;
        }

        var user = new User
        {
            Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
            UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
            EmailConfirmed = true
        };

        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        
        var createResult = await _userManager.CreateAsync(user);
        if (!createResult.Succeeded) 
            throw new GoogleAuthFailedException();
        
        var addRoleResult = await _userManager.AddToRoleAsync(user, UserRoles.CompanyOwner);
        if (!addRoleResult.Succeeded)
            throw new AddToRoleException();
        
        var addEmailClaimResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
        if (!addEmailClaimResult.Succeeded)
            throw new AddClaimException();
        
        var addNameClaimResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        if (!addNameClaimResult.Succeeded)
            throw new AddClaimException();

        var addLoginResult = await _userManager.AddLoginAsync(user, info);
        if (!addLoginResult.Succeeded)
            throw new GoogleAuthFailedException();
        
        var roles = await _userManager.GetRolesAsync(user);

        token = await _tokenService.GenerateGoogleAccessToken(info.Principal, user.Id, user.Email, roles);
        refreshToken = _tokenService.GenerateRefreshToken();

        token.RefreshToken = refreshToken;
        user.AddRefreshToken(refreshToken);
            
        _context.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        
        await transaction.CommitAsync(cancellationToken);
        
        return token;
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