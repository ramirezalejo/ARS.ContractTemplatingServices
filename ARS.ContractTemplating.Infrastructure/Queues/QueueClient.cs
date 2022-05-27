using System.Diagnostics.CodeAnalysis;
using ARS.ContractTemplating.Domain.Interfaces;
using ARS.ContractTemplating.Domain.Interfaces.Infrastructure;
using Azure.Storage.Queues;

namespace ARS.ContractTemplating.Infrastructure.Queues;

/// <summary>
/// Wrapper for Queue client from SDK
/// </summary>
[ExcludeFromCodeCoverage]
public class QueueClient : Azure.Storage.Queues.QueueClient, IQueueClient
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