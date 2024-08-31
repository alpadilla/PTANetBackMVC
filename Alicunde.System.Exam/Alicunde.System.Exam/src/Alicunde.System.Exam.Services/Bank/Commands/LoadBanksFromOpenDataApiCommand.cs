using Alicunde.System.Exam.Contracts;
using Alicunde.System.Exam.EntityFrameworkCore.Repositories;
using Alicunde.System.Exam.Services.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Alicunde.System.Exam.Services.Bank.Commands;

public class LoadBanksFromOpenDataApiCommand : IRequest<Unit>
{
}

public class LoadBanksFromOpenDataApiCommandHandler : IRequestHandler<LoadBanksFromOpenDataApiCommand, Unit>
{
    #region Props

    private readonly IRepository<Domain.Bank> _bankRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<LoadBanksFromOpenDataApiCommandHandler> _logger;
    private readonly IOpenDataApiService _openDataApiService;

    #endregion

    #region Ctor

    public LoadBanksFromOpenDataApiCommandHandler(
        IRepository<Domain.Bank> repository, 
        IMediator mediator, 
        ILogger<LoadBanksFromOpenDataApiCommandHandler> logger, 
        IOpenDataApiService openDataApiService
    )
    {
        _bankRepository = repository;
        _mediator = mediator;
        _logger = logger;
        _openDataApiService = openDataApiService;
    }

    #endregion
    
    public async Task<Unit> Handle(LoadBanksFromOpenDataApiCommand request, CancellationToken cancellationToken)
    {
        var openDataBankDtos = await _openDataApiService.GetBanksAsync();
        var bankCreateDtos = openDataBankDtos.ToIEnumerableBankCreateDto();
        
        foreach (var bankCreateDto in bankCreateDtos)
        {
            var bank = _bankRepository.Query().FirstOrDefault(x => x.Name == bankCreateDto.Name);
            if (bank is null)
            {
                try
                {
                    await _mediator.Send(new CreateBankCommand(bankCreateDto), cancellationToken);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Error while creating bank: {bankCreateDto.Name}");
                }
                continue;
            }

            try
            {
                var newBankInfo = bankCreateDto.ToUpdateDto();
                newBankInfo.CreatedAt = bank.CreatedAt;
                newBankInfo.DeleteAt = bank.DeleteAt;
                await _mediator.Send(new UpdateBankCommand(bank.Id, newBankInfo), cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while updating bank: {bankCreateDto.Name}");
            }
        }
        
        return Unit.Value;
    }
}