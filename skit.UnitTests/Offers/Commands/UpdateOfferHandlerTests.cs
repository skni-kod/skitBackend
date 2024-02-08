using skit.Application.Offers.Commands.UpdateOffer;
using skit.Core.Addresses.Entities;
using skit.Core.Addresses.Repositories;
using skit.Core.Common.Services;
using skit.Core.Identity.Exceptions;
using skit.Core.Offers.Entities;
using skit.Core.Offers.Enums;
using skit.Core.Offers.Exceptions;
using skit.Core.Offers.Repositories;
using skit.Core.Salaries.Entities;
using skit.Core.Salaries.Enums;
using skit.Core.Salaries.Exceptions;
using skit.Core.Technologies.Entities;
using skit.Core.Technologies.Repositories;

namespace skit.UnitTests.Offers.Commands;

public class UpdateOfferHandlerTests
{
    private readonly IOfferRepository _offerRepository = Substitute.For<IOfferRepository>();
    private readonly IAddressRepository _addressRepository = Substitute.For<IAddressRepository>();
    private readonly ITechnologyRepository _technologyRepository = Substitute.For<ITechnologyRepository>();
    private readonly ICurrentUserService _currentUserService = Substitute.For<ICurrentUserService>();
    
    [Fact]
    public async Task Handle_Should_ThrowException_WhenEmailIsNotConfirmed()
    {
        // Arrange
        var command = new UpdateOfferCommand
        (
            "Title",
            "Description",
            DateTimeOffset.Now,
            DateTimeOffset.Now.AddDays(30),
            OfferStatus.Draft,
            new List<OfferSeniority> { OfferSeniority.Junior },
            new List<OfferWorkLocation> { OfferWorkLocation.Remote },
            new List<UpdateOfferSalaries> { new UpdateOfferSalaries(1000, 2000, SalaryEmploymentType.Permanent) },
            new List<Guid> { Guid.NewGuid() },
            new List<Guid> { Guid.NewGuid() }
        );

        var handler = new UpdateOfferHandler(_offerRepository, _addressRepository, _currentUserService, _technologyRepository);
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
        var command = new UpdateOfferCommand
        (
            "Title",
            "Description",
            DateTimeOffset.Now,
            DateTimeOffset.Now.AddDays(30),
            OfferStatus.Draft,
            new List<OfferSeniority> { OfferSeniority.Junior },
            new List<OfferWorkLocation> { OfferWorkLocation.Remote },
            new List<UpdateOfferSalaries> { new UpdateOfferSalaries(1000, 2000, SalaryEmploymentType.Permanent) },
            new List<Guid> { Guid.NewGuid() },
            new List<Guid> { Guid.NewGuid() }
        );

        var handler = new UpdateOfferHandler(_offerRepository, _addressRepository, _currentUserService, _technologyRepository);
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
        var command = new UpdateOfferCommand
        (
            "Title",
            "Description",
            DateTimeOffset.Now,
            DateTimeOffset.Now.AddDays(30),
            OfferStatus.Draft,
            new List<OfferSeniority> { OfferSeniority.Junior },
            new List<OfferWorkLocation> { OfferWorkLocation.Remote },
            new List<UpdateOfferSalaries> { new UpdateOfferSalaries(1000, 2000, SalaryEmploymentType.Permanent) },
            new List<Guid> { Guid.NewGuid() },
            new List<Guid> { Guid.NewGuid() }
        );
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
        
        var handler = new UpdateOfferHandler(_offerRepository, _addressRepository, _currentUserService, _technologyRepository);
        _currentUserService.IsEmailConfirmedAsync(Arg.Any<CancellationToken>()).Returns(true);
        _offerRepository.GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(offer);
        _currentUserService.CompanyId.Returns(Guid.NewGuid());
        
        // Act
        var exception = await Assert.ThrowsAsync<OfferForbiddenException>(() => handler.Handle(command, CancellationToken.None));
        
        // Assert
        exception.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Handle_Should_ThrowException_WhenSalariesHaveDuplicateEmploymentType()
    {
        // Arrange
        var command = new UpdateOfferCommand
        (
            "Title",
            "Description",
            DateTimeOffset.Now,
            DateTimeOffset.Now.AddDays(30),
            OfferStatus.Draft,
            new List<OfferSeniority> { OfferSeniority.Junior },
            new List<OfferWorkLocation> { OfferWorkLocation.Remote },
            new List<UpdateOfferSalaries>
            {
                new UpdateOfferSalaries(1000, 2000, SalaryEmploymentType.Permanent),
                new UpdateOfferSalaries(2000, 3000, SalaryEmploymentType.Permanent)
            },
            new List<Guid> { Guid.NewGuid() },
            new List<Guid> { Guid.NewGuid() }
        );
        var companyId = Guid.NewGuid();
        var offer = Offer.Create(
            "Title",
            "Description",
            DateTimeOffset.Now,
            DateTimeOffset.Now,
            OfferStatus.Draft,
            OfferSeniority.Junior,
            OfferWorkLocation.Remote,
            companyId,
            new List<Salary>(),
            new List<Address>(),
            new List<Technology>()
        );
        
        var handler = new UpdateOfferHandler(_offerRepository, _addressRepository, _currentUserService, _technologyRepository);
        _currentUserService.IsEmailConfirmedAsync(Arg.Any<CancellationToken>()).Returns(true);
        _offerRepository.GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(offer);
        _currentUserService.CompanyId.Returns(companyId);
        
        // Act
        var exception = await Assert.ThrowsAsync<DuplicateEmploymentTypeException>(() => handler.Handle(command, CancellationToken.None));
        
        // Assert
        exception.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Handle_Should_UpdateOffer_WhenCommandIsValid()
    {
        // Arrange
        var command = new UpdateOfferCommand
        (
            "Title Updated",
            "Description",
            DateTimeOffset.Now,
            DateTimeOffset.Now.AddDays(30),
            OfferStatus.Draft,
            new List<OfferSeniority> { OfferSeniority.Junior },
            new List<OfferWorkLocation> { OfferWorkLocation.Remote },
            new List<UpdateOfferSalaries> { new UpdateOfferSalaries(1000, 2000, SalaryEmploymentType.Permanent) },
            new List<Guid> { Guid.NewGuid() },
            new List<Guid> { Guid.NewGuid() }
        );
        var companyId = Guid.NewGuid();
        var offer = Offer.Create(
            "Title",
            "Description",
            DateTimeOffset.Now,
            DateTimeOffset.Now,
            OfferStatus.Draft,
            OfferSeniority.Junior,
            OfferWorkLocation.Remote,
            companyId,
            new List<Salary>(),
            new List<Address>(),
            new List<Technology>()
        );
        
        var handler = new UpdateOfferHandler(_offerRepository, _addressRepository, _currentUserService, _technologyRepository);
        _currentUserService.IsEmailConfirmedAsync(Arg.Any<CancellationToken>()).Returns(true);
        _offerRepository.GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(offer);
        _currentUserService.CompanyId.Returns(companyId);
        
        // Act
        await handler.Handle(command, CancellationToken.None);
        
        // Assert
        await _offerRepository.Received().UpdateAsync(Arg.Is<Offer>(o => o.Title == command.Title), Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task Handle_Should_UpdateOffer_WhenCommandIsValid_WithMultipleSalaries()
    {
        // Arrange
        var command = new UpdateOfferCommand
        (
            "Title Updated",
            "Description",
            DateTimeOffset.Now,
            DateTimeOffset.Now.AddDays(30),
            OfferStatus.Draft,
            new List<OfferSeniority> { OfferSeniority.Junior },
            new List<OfferWorkLocation> { OfferWorkLocation.Remote },
            new List<UpdateOfferSalaries>
            {
                new UpdateOfferSalaries(1000, 2000, SalaryEmploymentType.B2B),
                new UpdateOfferSalaries(2000, 3000, SalaryEmploymentType.Permanent)
            },
            new List<Guid> { Guid.NewGuid() },
            new List<Guid> { Guid.NewGuid() }
        );
        var companyId = Guid.NewGuid();
        var offer = Offer.Create(
            "Title",
            "Description",
            DateTimeOffset.Now,
            DateTimeOffset.Now,
            OfferStatus.Draft,
            OfferSeniority.Junior,
            OfferWorkLocation.Remote,
            companyId,
            new List<Salary>(),
            new List<Address>(),
            new List<Technology>()
        );
        
        var handler = new UpdateOfferHandler(_offerRepository, _addressRepository, _currentUserService, _technologyRepository);
        _currentUserService.IsEmailConfirmedAsync(Arg.Any<CancellationToken>()).Returns(true);
        _offerRepository.GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(offer);
        _currentUserService.CompanyId.Returns(companyId);
        
        // Act
        await handler.Handle(command, CancellationToken.None);
        
        // Assert
        await _offerRepository.Received().UpdateAsync(Arg.Is<Offer>(o => o.Salaries.Count == 2), Arg.Any<CancellationToken>());
    }
}