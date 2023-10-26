using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using skit.Core.Salaries.Entities;

namespace skit.Infrastructure.DAL.Salaries.Configurations;

public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
{
    public void Configure(EntityTypeBuilder<Salary> builder)
    {
        builder.HasQueryFilter(x => x.DeletedById == null);
    }
}