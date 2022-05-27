namespace ARS.ContractTemplating.Domain.Interfaces.Infrastructure;
/// <summary>
/// Wrapper for Queue interactions
/// </summary>
public interface IQueueClient
{
    /// <summary>
    /// Publishes message to the queue
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    Task SendAsync(string message);
}