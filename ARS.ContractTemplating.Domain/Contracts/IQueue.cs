namespace ARS.ContractTemplating.Domain.Contracts;
/// <summary>
/// Wrapper for Queue interactions
/// </summary>
public interface IQueue
{
    /// <summary>
    /// Publishes message to the queue
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    Task SendAsync(string message);
}