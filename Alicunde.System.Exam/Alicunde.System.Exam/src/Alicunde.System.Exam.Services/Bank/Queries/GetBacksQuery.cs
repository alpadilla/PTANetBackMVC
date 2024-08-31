using Alicunde.System.Exam.Contracts.Bank;
using Alicunde.System.Exam.EntityFrameworkCore.Repositories;
using Alicunde.System.Exam.Services.Mappers;
using MediatR;

namespace Alicunde.System.Exam.Services.Bank.Queries;

public class GetBacksQuery : IRequest<IEnumerable<BankDto>>
{
}

public class GetBacksQueryHandler : IRequestHandler<GetBacksQuery, IEnumerable<BankDto>>
{
    #region Props

    private readonly IRepository<Domain.Bank> _bankRepository;

    public GetBacksQueryHandler(IRepository<Domain.Bank> bankRepository)
    {
        _bankRepository = bankRepository;
    }

    #endregion
    
    public async Task<IEnumerable<BankDto>> Handle(GetBacksQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var banks = await _bankRepository.GetAll();
            return banks.ToGeneralDtos();
        }
        catch (Exception e)
        {
            return new List<BankDto>();
        }
    }
}