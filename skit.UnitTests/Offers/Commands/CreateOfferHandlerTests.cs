using skit.Application.Offers.Commands.CreateOffer;
using skit.Core.Addresses.Repositories;
using skit.Core.Common.Services;
using skit.Core.Identity.Exceptions;
using skit.Core.Offers.Enums;
using skit.Core.Offers.Repositories;
using skit.Core.Salaries.Enums;
using skit.Core.Salaries.Exceptions;
using skit.Core.Technologies.Repositories;

namespace skit.UnitTests.Offers.Commands;

public class CreateOfferHandlerTests
{
    private readonly IOfferRepository _offerRepository = Substitute.For<IOfferRepository>();
    private readonly IAddressRepository _addressRepository = Substitute.For<IAddressRepository>();
    private readonly ITechnologyRepository _technologyRepository = Substitute.For<ITechnologyRepository>();
    private readonly ICurrentUserService _currentUserService = Substitute.For<ICurrentUserService>();

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

        var handler = new CreateOfferHandler(_offerRepository, _addressRepository, _currentUserService, _technologyRepository);
        _currentUserService.IsEmailConfirmedAsync(Arg.Any<CancellationToken>()).Returns(false);
        
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
        
        var handler = new CreateOfferHandler(_offerRepository, _addressRepository, _currentUserService, _technologyRepository);
        _currentUserService.IsEmailConfirmedAsync(Arg.Any<CancellationToken>()).Returns(true);
        
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
        
        var handler = new CreateOfferHandler(_offerRepository, _addressRepository, _currentUserService, _technologyRepository);
        _currentUserService.IsEmailConfirmedAsync(Arg.Any<CancellationToken>()).Returns(true);
        
        // Act
        var response = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        response.Should().NotBeNull();
    }
}