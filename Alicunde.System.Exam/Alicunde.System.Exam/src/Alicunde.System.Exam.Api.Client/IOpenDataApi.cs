using Alicunde.System.Exam.Contracts.OpenData;
using Refit;

namespace Alicunde.System.Exam.Api.Client;

public interface IOpenDataApi
{
    [Get("/EXP06/Banks")]
    Task<IApiResponse<IEnumerable<BankOpenDataDto>>> GetBanks();
}