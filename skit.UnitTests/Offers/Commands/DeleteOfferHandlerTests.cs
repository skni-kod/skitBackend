using skit.Application.Offers.Commands.DeleteOffer;
using skit.Core.Addresses.Entities;
using skit.Core.Common.Services;
using skit.Core.Identity.Exceptions;
using skit.Core.Offers.Entities;
using skit.Core.Offers.Enums;
using skit.Core.Offers.Exceptions;
using skit.Core.Offers.Repositories;
using skit.Core.Salaries.Entities;
using skit.Core.Technologies.Entities;

namespace skit.UnitTests.Offers.Commands;

public class DeleteOfferHandlerTests
{
    private readonly IOfferRepository _offerRepository = Substitute.For<IOfferRepository>();
    private readonly ICurrentUserService _currentUserService = Substitute.For<ICurrentUserService>();

    [Fact]
    public async Task Handle_Should_ThrowException_WhenEmailIsNotConfirmed()
    {
        // Arrange
        var command = new DeleteOfferCommand(Guid.NewGuid());

        var handler = new DeleteOfferHandler(_offerRepository, _currentUserService);
        _currentUserService.IsEmailConfirmedAsync(Arg.Any<CancellationToken>()).Returns(false);
        
        // Act
        var exception = await Assert.ThrowsAsync<UnconfirmedEmailException>(() => handler.Handle(command, CancellationToken.None));
        
        // Assert
        exception.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Handle_Should_ThrowException_WhenOfferIsNotFound()
    {
        // Arrange
        var command = new DeleteOfferCommand(Guid.NewGuid());

        var handler = new DeleteOfferHandler(_offerRepository, _currentUserService);
        _currentUserService.IsEmailConfirmedAsync(Arg.Any<CancellationToken>()).Returns(true);
        _offerRepository.GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns((Offer?)null);
        
        // Act
        var exception = await Assert.ThrowsAsync<OfferNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        
        // Assert
        exception.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Handle_Should_ThrowException_WhenOfferIsForbidden()
    {
        // Arrange
        var command = new DeleteOfferCommand(Guid.NewGuid());
        var offer = Offer.Create(
            "Title",
            "Description",
            DateTimeOffset.Now,
            DateTimeOffset.Now,
            OfferStatus.Draft,
            OfferSeniority.Junior,
            OfferWorkLocation.Remote,
            Guid.NewGuid(),
            new List<Salary>(),
            new List<Address>(),
            new List<Technology>()
        );
        
        var handler = new DeleteOfferHandler(_offerRepository, _currentUserService);
        _currentUserService.IsEmailConfirmedAsync(Arg.Any<CancellationToken>()).Returns(true);
        _offerRepository.GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(offer);
        _currentUserService.CompanyId.Returns(Guid.NewGuid());
        
        // Act
        var exception = await Assert.ThrowsAsync<OfferForbiddenException>(() => handler.Handle(command, CancellationToken.None));
        
        // Assert
        exception.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Handle_Should_DeleteOffer_WhenCommandIsValid()
    {
        // Arrange
        var command = new DeleteOfferCommand(Guid.NewGuid());
        var offer = Offer.Create(
            "Title",
            "Description",
            DateTimeOffset.Now,
            DateTimeOffset.Now,
            OfferStatus.Draft,
            OfferSeniority.Junior,
            OfferWorkLocation.Remote,
            Guid.NewGuid(),
            new List<Salary>(),
            new List<Address>(),
            new List<Technology>()
        );
        
        var handler = new DeleteOfferHandler(_offerRepository, _currentUserService);
        _currentUserService.IsEmailConfirmedAsync(Arg.Any<CancellationToken>()).Returns(true);
        _offerRepository.GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(offer);
        _currentUserService.CompanyId.Returns(offer.CompanyId);
        
        // Act
        await handler.Handle(command, CancellationToken.None);
        
        // Assert
        await _offerRepository.Received(1).DeleteAsync(offer, Arg.Any<CancellationToken>());
    }
}