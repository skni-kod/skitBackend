using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using skit.Core.Companies.Entities;
using skit.Core.Identity.Entities;

namespace skit.Infrastructure.DAL.Companies.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder
            .HasOne(x => x.Owner)
            .WithOne(x => x.Company)
            .HasForeignKey<User>(u => u.CompanyId);

        builder.HasQueryFilter(x => x.DeletedById == null);
    }
}