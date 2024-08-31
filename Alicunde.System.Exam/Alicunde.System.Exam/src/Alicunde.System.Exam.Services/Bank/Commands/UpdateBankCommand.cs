using Alicunde.System.Exam.Contracts.Bank;
using Alicunde.System.Exam.EntityFrameworkCore.Repositories;
using Alicunde.System.Exam.Services.Mappers;
using MediatR;

namespace Alicunde.System.Exam.Services.Bank.Commands;

public class UpdateBankCommand : IRequest<BankDto>
{
    public Guid Id { get; set; }
    public BankUpdateDto BankUpdateDto { get; set; }
    
    public UpdateBankCommand(Guid id, BankUpdateDto bankUpdateDto)
    {
        Id = id;
        BankUpdateDto = bankUpdateDto;
    }
}

public class UpdateBankCommandHandler : IRequestHandler<UpdateBankCommand, BankDto>
{
    #region Props

    private readonly IRepository<Domain.Bank> _bankRepository;

    #endregion

    #region Ctor

    public UpdateBankCommandHandler(IRepository<Domain.Bank> repository)
    {
        _bankRepository = repository;
    }

    #endregion
    
    public async Task<BankDto> Handle(UpdateBankCommand request, CancellationToken cancellationToken)
    {
        var newBankInfo = request.BankUpdateDto.ToEntity();
        var updatedBank = await _bankRepository.UpdateAsync(newBankInfo, request.Id);
        return updatedBank.ToGeneralDto();
    }
}