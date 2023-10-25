using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using skit.Core.Offers.Entities;

namespace skit.Infrastructure.DAL.Offers.Configurations;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder
            .HasMany(x => x.Addresses)
            .WithMany(x => x.Offers)
            .UsingEntity("OffersAddresses");
        
        builder.HasQueryFilter(x => x.DeletedById == null);
    }
}