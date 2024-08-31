using Alicunde.System.Exam.Contracts.Bank;
using Alicunde.System.Exam.EntityFrameworkCore.Repositories;
using Alicunde.System.Exam.Services.Mappers;
using MediatR;

namespace Alicunde.System.Exam.Services.Bank.Commands;

public class DeleteBankCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    
    public DeleteBankCommand(Guid id)
    {
        Id = id;
    }
}

public class DeleteBankCommandHandler : IRequestHandler<DeleteBankCommand, Unit>
{
    #region Props

    private readonly IRepository<Domain.Bank> _bankRepository;

    #endregion

    #region Ctor

    public DeleteBankCommandHandler(IRepository<Domain.Bank> repository)
    {
        _bankRepository = repository;
    }

    #endregion
    
    public async Task<Unit> Handle(DeleteBankCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var foundBank = await _bankRepository.GetByIdAsync(request.Id);
            await _bankRepository.DeleteAsync(foundBank.Id);
            return Unit.Value;
        }
        catch (KeyNotFoundException)
        {
            return Unit.Value;
        }
    }
}