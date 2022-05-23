using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using ARS.ContractTemplating.Domain.Contracts.Services;
using ARS.ContractTemplating.Domain.Models.Messages;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ARS.ContractTemplating.Functions;

/// <summary>
/// ContractTemplateRequest Azure Function
/// </summary>
public class ContractTemplateRequest
{
    private readonly IContractsService _contractService;
    private readonly ILogger _logger;

    /// <summary>
    /// ContractTemplateRequest Azure Function Constructor
    /// </summary>
    /// <param name="contractService"></param>
    /// <param name="logger"></param>
    public ContractTemplateRequest(IContractsService contractService, ILogger logger)
    {
        _contractService = contractService;
        _logger = logger;
    }

    /// <summary>
    /// Function triggered by new message ont he queue
    /// </summary>
    /// <param name="myQueueItem"></param>
    /// <param name="cancellationToken"></param>
    [FunctionName("ContractTemplateRequest")]
    public async Task RunAsync([QueueTrigger("ContractRequest", Connection = "StorageConnectionString")] string myQueueItem,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# Queue trigger function processed: {MyQueueItem}", myQueueItem);
        var request = JsonSerializer.Deserialize<ContractRequestMessage>(myQueueItem);
        await _contractService.GenerateContract(request ?? throw new InvalidOperationException("Queue Message cannot be deserialized to ContractRequestMessage object"));
    }
}