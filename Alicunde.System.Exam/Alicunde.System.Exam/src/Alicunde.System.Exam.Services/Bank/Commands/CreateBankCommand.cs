using Alicunde.System.Exam.Contracts.Bank;
using Alicunde.System.Exam.EntityFrameworkCore.Repositories;
using Alicunde.System.Exam.Services.Mappers;
using MediatR;

namespace Alicunde.System.Exam.Services.Bank.Commands;

public class CreateBankCommand : IRequest<BankDto>
{
    public BankCreateDto BankCreateDto { get; set; }
    
    public CreateBankCommand(BankCreateDto bankCreateDto)
    {
        BankCreateDto = bankCreateDto;
    }
}

public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand, BankDto>
{
    #region Props

    private readonly IRepository<Domain.Bank> _bankRepository;

    #endregion

    #region Ctor

    public CreateBankCommandHandler(IRepository<Domain.Bank> repository)
    {
        _bankRepository = repository;
    }

    #endregion
    
    public async Task<BankDto> Handle(CreateBankCommand request, CancellationToken cancellationToken)
    {
        var createdBank = await _bankRepository.AddAsync(request.BankCreateDto.ToEntity());
        return createdBank.ToGeneralDto();
    }
}