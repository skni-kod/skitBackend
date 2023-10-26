using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using skit.Core.Addresses.Entities;

namespace skit.Infrastructure.DAL.Addresses.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasQueryFilter(x => x.DeletedById == null);
    }
}