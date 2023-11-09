using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using skit.Core.Addresses.Entities;
using skit.Core.Common.Services;
using skit.Core.Companies.Entities;
using skit.Core.Identity.Entities;
using skit.Core.JobApplications.Entities;
using skit.Core.Offers.Entities;
using skit.Core.Salaries.Entities;
using skit.Infrastructure.DAL.Addresses.Configurations;
using skit.Infrastructure.DAL.Companies.Configurations;
using skit.Infrastructure.DAL.JobApplications.Configurations;
using skit.Infrastructure.DAL.Offers.Configurations;
using skit.Infrastructure.DAL.Salaries.Configurations;
using skit.Shared.Abstractions.Entities;

namespace skit.Infrastructure.DAL.EF.Context;

public class EFContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    private readonly IDateService _dateService;
    private readonly ICurrentUserService _currentUserService;
    
    public DbSet<Address> Addresses { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Salary> Salaries { get; set; }

    private readonly Guid _userId = Guid.Empty;
    
    public EFContext(DbContextOptions<EFContext> options) : base(options) {}

    public EFContext(DbContextOptions<EFContext> options, IDateService dateService, ICurrentUserService currentUserService) : base(options)
    {
        _dateService = dateService;
        _currentUserService = currentUserService;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new JobApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new OfferConfiguration());
        modelBuilder.ApplyConfiguration(new SalaryConfiguration());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if(entry.Entity is Entity)
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["CreatedById"] = _currentUserService.UserId;
                        entry.CurrentValues["CreatedAt"] = _dateService.CurrentOffsetDate();
                        entry.CurrentValues["DeletedById"] = null;
                        entry.CurrentValues["DeletedAt"] = null;
                        break;
                    
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["DeletedById"] = _currentUserService.UserId;
                        entry.CurrentValues["DeletedAt"] = _dateService.CurrentOffsetDate();
                        break;
                }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}