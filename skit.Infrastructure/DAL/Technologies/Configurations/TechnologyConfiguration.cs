using Microsoft.EntityFrameworkCore.Metadata.Builders;
using skit.Core.Salaries.Entities;
using skit.Core.Technologies.Entities;

namespace skit.Infrastructure.DAL.Technologies.Configurations;

public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
{
    public void Configure(EntityTypeBuilder<Technology> builder)
    {
        builder.HasQueryFilter(x => x.DeletedById == null);
    }
}