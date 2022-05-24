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
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Dictionary<string, string?>> AnalyzeDocumentFromUrlAsync(string url, CancellationToken cancellationToken);

    /// <summary>
    /// Extract data from document/image
    /// </summary>
    /// <param name="url"></param>
    /// <param name="locale"></param>
    /// <param name="pages"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Dictionary<string, string?>> AnalyzeDocumentFromUrlWithSettingsAsync(string url, string? locale,
        IList<string> pages, CancellationToken cancellationToken);

    /// <summary>
    /// Extract data from doc/image stream 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Dictionary<string, string?>>
        AnalyzeDocumentFromStreamAsync(Stream stream, CancellationToken cancellationToken);

    /// <summary>
    /// Extract data from doc/image stream passing settings
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="locale"></param>
    /// <param name="pages"></param>
    /// <returns></returns>
    Task<Dictionary<string, string?>> AnalyzeDocumentFromStreamAsync(Stream stream, CancellationToken cancellationToken,
        string? locale, IList<string> pages);

    /// <summary>
    /// Extract data from document/image
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="locale"></param>
    /// <param name="pages"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Dictionary<string, string?>> AnalyzeDocumentFromUriWithSettingsAsync(Uri uri, string? locale,
        IList<string> pages, CancellationToken cancellationToken);
}