using ARS.ContractTemplating.Domain.Contracts;
using Microsoft.Extensions.Logging;

namespace ARS.ContractTemplating.Services;

/// <summary>
/// Contracts Service
/// </summary>
public class ContractsService
{
    private readonly ICognitiveServicesClient _cognitiveServicesClient;
    private readonly ILogger _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="cognitiveServicesClient"></param>
    /// <param name="logger"></param>
    public ContractsService(ICognitiveServicesClient cognitiveServicesClient, ILogger logger)
    {
        _cognitiveServicesClient = cognitiveServicesClient;
        _logger = logger;
    }
    
    //public async Task GenerateContract()
}