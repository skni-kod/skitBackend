using MediatR;
using skit.Core.Offers.Repositories;
using skit.Core.Salaries.Entities;
using skit.Core.Salaries.Exceptions;
using skit.Core.Salaries.Repositories;

namespace skit.Application.Salaries.Commands.UpdateSalariesFromList;

public sealed class UpdateSalariesFromListHandler : IRequestHandler<UpdateSalariesFromListCommand>
{
    private readonly ISalaryRepository _salaryRepository;
    private readonly IOfferRepository _offerRepository;

    public UpdateSalariesFromListHandler(ISalaryRepository salaryRepository, IOfferRepository offerRepository)
    {
        _salaryRepository = salaryRepository;
        _offerRepository = offerRepository;
    }

    public async Task Handle(UpdateSalariesFromListCommand command, CancellationToken cancellationToken)
    {
        // Nie wiem czy to jest potrzebne bo wsm zalatwilem to tym drugim Exceptionem
        // Pytanie czy taka infomacja dodatkowa jest nam potrzebna, ze po prostu akurat ta konkretna pensja
        // istnieje ale nie nalezy do tej oferty. Kwestia czytelnosci chyba ale szkoda robic dodatkowych operacji na bazie
        // wsm i tak nie do konca ta metodka dziala XD
        // var salariesExistInOffer =
        //     await _offerRepository.SalariesExistInOffer(command.OfferId, command.Salaries.Select(salary => salary.SalaryId).ToList(), cancellationToken);
        //
        // if (salariesExistInOffer == false)
        //     throw new SalaryDoesNotExistInOfferException();
        
        var salariesIds = command.Salaries.Select(salary => salary.SalaryId).ToList();
        var salaries = await _salaryRepository.GetListAsyncForOffer(command.OfferId, salariesIds, cancellationToken);

        if (salaries.Count != command.Salaries.Count())
            throw new NotAllSalariesFoundException();

        foreach (var salary in salaries)
        {
            var salaryToUpdate = command.Salaries.Single(salaryToUpdate => salaryToUpdate.SalaryId == salary.Id);
            salary.Update(salaryToUpdate.SalaryFrom, salaryToUpdate.SalaryTo);
        }

        //Moze feature add or update
        await _salaryRepository.UpdateRangeAsync(salaries, cancellationToken);
    }
}