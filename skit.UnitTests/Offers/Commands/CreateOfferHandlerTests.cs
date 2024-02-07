using skit.Application.Offers.Commands.CreateOffer;
using skit.Core.Addresses.Repositories;
using skit.Core.Common.Services;
using skit.Core.Identity.Exceptions;
using skit.Core.Offers.Entities;
using skit.Core.Offers.Enums;
using skit.Core.Offers.Repositories;
using skit.Core.Salaries.Enums;
using skit.Core.Salaries.Exceptions;
using skit.Core.Technologies.Repositories;

namespace skit.UnitTests.Offers.Commands;

public class CreateOfferHandlerTests
{
    private readonly Mock<IOfferRepository> _offerRepository = new();
    private readonly Mock<IAddressRepository> _addressRepository = new();
    private readonly Mock<ITechnologyRepository> _technologyRepository = new();
    private readonly Mock<ICurrentUserService> _currentUserService = new();

    [Fact]
    public async Task Handle_Should_ThrowException_WhenEmailIsNotConfirmed()
    {
        // Arrange
        var command = new CreateOfferCommand
        {
            Title = "Title",
            Status = OfferStatus.Draft,
            Seniorities = new List<OfferSeniority> { OfferSeniority.Junior },
            WorkLocations = new List<OfferWorkLocation> { OfferWorkLocation.Remote },
            Salaries = new List<CreateOfferSalaries> { new CreateOfferSalaries(1000, 2000, SalaryEmploymentType.Permanent) },
            AddressIds = new List<Guid> { Guid.NewGuid() },
            TechnologyIds = new List<Guid> { Guid.NewGuid() }
        };

        var handler = new CreateOfferHandler(_offerRepository.Object, _addressRepository.Object, _currentUserService.Object, _technologyRepository.Object);
        _currentUserService.Setup(x => x.IsEmailConfirmedAsync(It.IsAny<CancellationToken>())).ReturnsAsync(false);
        
        // Act
        var exception = await Assert.ThrowsAsync<UnconfirmedEmailException>(() => handler.Handle(command, CancellationToken.None));
        
        // Assert
        exception.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Handle_Should_ThrowException_WhenSalariesHaveDuplicateEmploymentType()
    {
        // Arrange
        var command = new CreateOfferCommand
        {
            Title = "Title",
            Status = OfferStatus.Draft,
            Seniorities = new List<OfferSeniority> { OfferSeniority.Junior },
            WorkLocations = new List<OfferWorkLocation> { OfferWorkLocation.Remote },
            Salaries = new List<CreateOfferSalaries>
            {
                new CreateOfferSalaries(1000, 2000, SalaryEmploymentType.Permanent),
                new CreateOfferSalaries(2000, 3000, SalaryEmploymentType.Permanent)
            },
            AddressIds = new List<Guid> { Guid.NewGuid() },
            TechnologyIds = new List<Guid> { Guid.NewGuid() }
        };

        var handler = new CreateOfferHandler(_offerRepository.Object, _addressRepository.Object, _currentUserService.Object, _technologyRepository.Object);
        _currentUserService.Setup(x => x.IsEmailConfirmedAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
        
        // Act
        var exception = await Assert.ThrowsAsync<DuplicateEmploymentTypeException>(() => handler.Handle(command, CancellationToken.None));
        
        // Assert
        exception.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Handle_Should_AddOffer_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreateOfferCommand
        {
            Title = "Title",
            Status = OfferStatus.Draft,
            Seniorities = new List<OfferSeniority> { OfferSeniority.Junior },
            WorkLocations = new List<OfferWorkLocation> { OfferWorkLocation.Remote },
            Salaries = new List<CreateOfferSalaries> { new CreateOfferSalaries(1000, 2000, SalaryEmploymentType.Permanent) },
            AddressIds = new List<Guid> { Guid.NewGuid() },
            TechnologyIds = new List<Guid> { Guid.NewGuid() }
        };

        var handler = new CreateOfferHandler(_offerRepository.Object, _addressRepository.Object, _currentUserService.Object, _technologyRepository.Object);
        _currentUserService.Setup(x => x.IsEmailConfirmedAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _offerRepository.Setup(x => x.AddAsync(It.IsAny<Offer>(), It.IsAny<CancellationToken>())).ReturnsAsync(Guid.NewGuid());
        
        // Act
        var response = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        response.Should().NotBeNull();
    }
}