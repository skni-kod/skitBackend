using Microsoft.EntityFrameworkCore.Metadata.Builders;
using skit.Core.Files.Entities;

namespace skit.Infrastructure.DAL.Files.Configuration;

public sealed class FileConfiguration : IEntityTypeConfiguration<SystemFile>
{
    public void Configure(EntityTypeBuilder<SystemFile> builder)
    {
        builder.HasQueryFilter(x => x.DeletedById == null);
    }
}