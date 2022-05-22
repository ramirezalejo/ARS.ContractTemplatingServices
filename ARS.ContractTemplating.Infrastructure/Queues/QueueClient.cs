using ARS.ContractTemplating.Domain.Contracts;
using Azure.Storage.Queues;

namespace ARS.ContractTemplating.Infrastructure.Queues;

/// <summary>
/// Wrapper for Queue client from SDK
/// </summary>
public class QueueClient : Azure.Storage.Queues.QueueClient, IQueue
{
    /// <summary>
    /// Constructor matching the base
    /// </summary>
    /// <param name="connectionString"></param>
    /// <param name="queueName"></param>
    public QueueClient(string connectionString, string queueName) : base (connectionString, queueName, new QueueClientOptions(){ MessageEncoding = QueueMessageEncoding.Base64 })
    {
        
    }
    /// <summary>
    /// Send message to the queue
    /// </summary>
    /// <param name="message"></param>
    public async Task SendAsync(string message)
    {
        await base.SendMessageAsync(message);
    }
    
}