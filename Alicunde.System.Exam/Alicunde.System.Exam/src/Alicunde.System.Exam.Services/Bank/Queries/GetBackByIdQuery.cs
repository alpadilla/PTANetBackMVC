using Alicunde.System.Exam.Contracts.Bank;
using Alicunde.System.Exam.EntityFrameworkCore.Repositories;
using Alicunde.System.Exam.Services.Mappers;
using MediatR;

namespace Alicunde.System.Exam.Services.Bank.Queries;

public class GetBackByIdQuery : IRequest<BankDto?>
{
    public Guid Id { get; set; }

    public GetBackByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetBackByIdQueryHandler : IRequestHandler<GetBackByIdQuery, BankDto?>
{
    #region Props

    private readonly IRepository<Domain.Bank> _bankRepository;

    public GetBackByIdQueryHandler(IRepository<Domain.Bank> bankRepository)
    {
        _bankRepository = bankRepository;
    }

    #endregion
    
    public async Task<BankDto?> Handle(GetBackByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var bank = await _bankRepository.GetByIdAsync(request.Id);
            return (BankDto)bank.ToGeneralDto();
        }
        catch (Exception e)
        {
            return null;
        }
    }
}