using System.Net;
using Alicunde.System.Exam.Contracts;
using Microsoft.Extensions.Logging;
using Refit;

namespace Alicunde.System.Exam.Services.Helpers;

public class ApiResponseManager<T> : IApiResponseManager<T>
{
    private readonly ILogger<ApiResponseManager<T>> _logger;

    public ApiResponseManager(ILogger<ApiResponseManager<T>> logger)
    {
        _logger = logger;
    }

    public T? GetResult(IApiResponse<T> apiResponse, string errorMessage)
    {
        if (apiResponse.IsSuccessStatusCode)
        {
            return apiResponse.Content;
        }

        if (apiResponse.StatusCode is HttpStatusCode.NoContent or HttpStatusCode.NotFound)
        {
            return default;
        }
        
        _logger.LogError(apiResponse.Error, errorMessage);

        return default;
    }
}