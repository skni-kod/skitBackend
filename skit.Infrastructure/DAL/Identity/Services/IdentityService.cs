using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
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

    public IdentityService(UserManager<User> userManager, EFContext context, SignInManager<User> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _context = context;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }
    
    public async Task SignUpCompany(string email, string companyName, string password, CancellationToken cancellationToken)
    {
        var userEmailIsNotUnique = await _userManager.Users.AnyAsync(x => x.Email == email, cancellationToken);
        
        if (userEmailIsNotUnique)
            throw new UserWithEmailExistsException();
        
        var companyNameIsNotUnique = await _context.Companies.AnyAsync(x => x.Name.Equals(companyName), cancellationToken);
        
        if (companyNameIsNotUnique)
            throw new CompanyWithNameExistsException();
        
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        
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
    }

    public async Task<JwtWebToken> SignIn(string email, string password, CancellationToken cancellationToken)
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

        return jwt;
    }
}