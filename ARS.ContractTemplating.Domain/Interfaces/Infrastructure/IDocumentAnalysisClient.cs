namespace ARS.ContractTemplating.Domain.Interfaces.Infrastructure;

/// <summary>
/// Document analysis contract
/// </summary>
public interface IDocumentAnalysisClient
{
    /// <summary>
    /// Extract data from document/image
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    Task<Dictionary<string, string?>> AnalyzeDocumentFromUriAsync(string url);

    /// <summary>
    /// Extract data from doc/image stream 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Dictionary<string, string?>>
        AnalyzeDocumentFromStreamAsync(Stream stream, CancellationToken cancellationToken);
}