using System.Threading.Tasks;
using System.Threading;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ARS.ContractTemplating.Functions;

/// <summary>
/// ContractTemplateRequest Azure Function
/// </summary>
public class ContractTemplateRequest
{
    private readonly ILogger _logger;

    /// <summary>
    /// ContractTemplateRequest Azure Function Constructor
    /// </summary>
    /// <param name="logger"></param>
    public ContractTemplateRequest(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Function triggered by new message ont he queue
    /// </summary>
    /// <param name="myQueueItem"></param>
    /// <param name="cancellationToken"></param>
    [FunctionName("ContractTemplateRequest")]
    public async Task RunAsync([QueueTrigger("myqueue", Connection = "%StorageConnectionString%")] string myQueueItem,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# Queue trigger function processed: {MyQueueItem}", myQueueItem);
        await Task.Delay(3000, cancellationToken);
    }
}