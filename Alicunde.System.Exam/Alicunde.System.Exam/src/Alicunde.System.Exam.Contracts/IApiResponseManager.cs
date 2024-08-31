using Refit;

namespace Alicunde.System.Exam.Contracts;

public interface IApiResponseManager<T>
{
    T? GetResult(IApiResponse<T> apiResponse, string errorMessage);
}