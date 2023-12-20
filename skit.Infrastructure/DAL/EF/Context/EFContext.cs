using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using skit.Core.Addresses.Entities;
using skit.Core.Common.Services;
using skit.Core.Companies.Entities;
using skit.Core.Files.Entities;
using skit.Core.Identity.Entities;
using skit.Core.JobApplications.Entities;
using skit.Core.Offers.Entities;
using skit.Core.Salaries.Entities;
using skit.Core.Technologies.Entities;
using skit.Infrastructure.DAL.Addresses.Configurations;
using skit.Infrastructure.DAL.Companies.Configurations;
using skit.Infrastructure.DAL.Files;
using skit.Infrastructure.DAL.Files.Configuration;
using skit.Infrastructure.DAL.JobApplications.Configurations;
using skit.Infrastructure.DAL.Offers.Configurations;
using skit.Infrastructure.DAL.Salaries.Configurations;
using skit.Infrastructure.DAL.Technologies.Configurations;
using skit.Shared.Abstractions.Entities;

namespace skit.Infrastructure.DAL.EF.Context;

public class EFContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    private readonly IDateService _dateService;

    public DbSet<Address> Addresses { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Salary> Salaries { get; set; }
    public DbSet<Technology> Technologies { get; set; }
    public DbSet<SystemFile> Files { get; set; }

    private readonly Guid _userId = Guid.Empty;
    
    public EFContext(DbContextOptions<EFContext> options) : base(options) {}

    public EFContext(DbContextOptions<EFContext> options, IDateService dateService, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _dateService = dateService;
        _ = Guid.TryParse(httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier),
            out _userId);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new JobApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new OfferConfiguration());
        modelBuilder.ApplyConfiguration(new SalaryConfiguration());
        modelBuilder.ApplyConfiguration(new TechnologyConfiguration());
        modelBuilder.ApplyConfiguration(new FileConfiguration());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if(entry.Entity is Entity)
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["CreatedById"] = _userId;
                        entry.CurrentValues["CreatedAt"] = _dateService.CurrentOffsetDate();
                        entry.CurrentValues["DeletedById"] = null;
                        entry.CurrentValues["DeletedAt"] = null;
                        break;
                    
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["DeletedById"] = _userId;
                        entry.CurrentValues["DeletedAt"] = _dateService.CurrentOffsetDate();
                        break;
                }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}