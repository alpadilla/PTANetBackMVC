using Alicunde.System.Exam.Contracts.OpenData;

namespace Alicunde.System.Exam.Contracts;

public interface IOpenDataApiService
{
    Task<IEnumerable<BankOpenDataDto>> GetBanksAsync();
}