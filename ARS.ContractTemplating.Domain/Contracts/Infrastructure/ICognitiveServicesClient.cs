namespace ARS.ContractTemplating.Domain.Contracts.Infrastructure;
/// <summary>
/// Contract to hide implementation for Cognitive service client
/// </summary>
public interface ICognitiveServicesClient
{
    /// <summary>
    /// Read test from image url
    /// </summary>
    /// <param name="sourceFileUrl"></param>
    /// <returns></returns>
    Task<List<string>> ReadFileUrl(string sourceFileUrl);
}