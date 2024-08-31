using Alicunde.System.Exam.Api.Client;
using Alicunde.System.Exam.Contracts;
using Alicunde.System.Exam.Contracts.OpenData;
using Microsoft.Extensions.Logging;

namespace Alicunde.System.Exam.Services.Services;

public class OpenDataApiService : IOpenDataApiService
{
    #region Props

    private readonly IOpenDataApi _openDataApiClient;
    private readonly ILogger<OpenDataApiService> _logger;
    private readonly IApiResponseManager<IEnumerable<BankOpenDataDto>> _apiResponseMananger;

    #endregion

    #region Ctor

    public OpenDataApiService(
        IOpenDataApi openDataApiClient, 
        ILogger<OpenDataApiService> logger, 
        IApiResponseManager<IEnumerable<BankOpenDataDto>> apiResponseMananger
    )
    {
        _openDataApiClient = openDataApiClient;
        _logger = logger;
        _apiResponseMananger = apiResponseMananger;
    }

    #endregion
    
    public virtual async Task<IEnumerable<BankOpenDataDto>> GetBanksAsync()
    {
        try
        {
            var apiResponse = await _openDataApiClient.GetBanks();

            return _apiResponseMananger.GetResult(
                apiResponse,
                "An error occurred while getting banks"
            )!;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while getting banks");
            throw;
        }
    }
}