using ARS.ContractTemplating.Domain.Contracts.Infrastructure;
using ARS.ContractTemplating.Domain.Contracts.Services;
using ARS.ContractTemplating.Domain.Enums;
using ARS.ContractTemplating.Domain.Models.Messages;
using Microsoft.Extensions.Logging;

namespace ARS.ContractTemplating.Services;

/// <summary>
/// Contracts Service
/// </summary>
public class ContractsService : IContractsService
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

    /// <summary>
    /// Generates the contract and uploads it to the storage
    /// </summary>
    /// <param name="requestMessage"></param>
    public async Task GenerateContract(ContractRequestMessage requestMessage)
    {
        var buyerFile = requestMessage?.Files?.FirstOrDefault(x => x.FileRoleType == FileRoleType.Buyer);
        var buyerDataFromFile = buyerFile is not null
            ? await _cognitiveServicesClient.ReadFileUrl(buyerFile.FilePath ?? throw new InvalidOperationException())
            : new List<string>();
    }
}