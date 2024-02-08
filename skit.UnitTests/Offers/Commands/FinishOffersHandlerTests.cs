using skit.Application.Offers.Commands.FinishOffers;
using skit.Core.Addresses.Entities;
using skit.Core.Common.Services;
using skit.Core.Offers.Entities;
using skit.Core.Offers.Enums;
using skit.Core.Offers.Repositories;
using skit.Core.Salaries.Entities;
using skit.Core.Salaries.Enums;
using skit.Core.Technologies.Entities;

namespace skit.UnitTests.Offers.Commands;

public class FinishOffersHandlerTests
{
    private readonly IOfferRepository _offerRepository = Substitute.For<IOfferRepository>();
    private readonly IDateService _dateService = Substitute.For<IDateService>();
    
    [Fact]
    public async Task Handle_Should_FinishOffers()
    {
        // Arrange
        var handler = new FinishOffersHandler(_offerRepository, _dateService);
        var offers = new List<Offer>
        {
            Offer.Create(
                "Title",
                "Description",
                DateTimeOffset.Now,
                DateTimeOffset.Now.AddDays(30),
                OfferStatus.Draft,
                OfferSeniority.Junior,
                OfferWorkLocation.Remote,
                Guid.NewGuid(),
                new List<Salary>(),
                new List<Address>(),
                new List<Technology>()
            )
        };
        _offerRepository.GetAllAsync(Arg.Any<CancellationToken>()).Returns(offers);
        _dateService.CurrentOffsetDate().Returns(DateTimeOffset.Now.AddDays(31));
        
        // Act
        await handler.Handle(new FinishOffersCommand(), CancellationToken.None);
        
        // Assert
        await _offerRepository.Received().UpdateAsync(Arg.Is<Offer>(o => o.Status == OfferStatus.Finished), Arg.Any<CancellationToken>());
    }
}