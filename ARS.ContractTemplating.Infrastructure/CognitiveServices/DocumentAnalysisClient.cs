using System.Diagnostics.CodeAnalysis;
using ARS.ContractTemplating.Domain.Interfaces.Infrastructure;
using Azure;

namespace ARS.ContractTemplating.Infrastructure.CognitiveServices;
using Azure = Azure.AI.FormRecognizer.DocumentAnalysis;

/// <summary>
/// Client for Azure FormRecognizer
/// </summary>
[ExcludeFromCodeCoverage]
public class DocumentAnalysisClient : Azure.DocumentAnalysisClient, IDocumentAnalysisClient
{
    /// <summary>
    /// Document Analysis Client 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="endpoint"></param>
    public DocumentAnalysisClient(string key, string endpoint):base(new Uri(endpoint), new AzureKeyCredential(key))
    {
        
    }

    /// <summary>
    /// Extract data from document/image
    /// </summary>
    /// <param name="url"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Dictionary<string, string?>> AnalyzeDocumentFromUrlAsync(string url, CancellationToken cancellationToken)
    {
        return await AnalyzeDocumentFromUrlWithSettingsAsync(url, null, new List<string>(), cancellationToken);
    }

    /// <summary>
    /// Triggers the analysis for the given Url
    /// </summary>
    /// <param name="url"></param>
    /// <param name="locale"></param>
    /// <param name="pages"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Dictionary<string, string?>> AnalyzeDocumentFromUrlWithSettingsAsync(string url, string? locale, IList<string> pages, CancellationToken cancellationToken)
    {
        Uri fileUri = new Uri (url);

        return await AnalyzeDocumentFromUriWithSettingsAsync(fileUri, locale, pages, cancellationToken);

    }
    
    /// <summary>
    /// Triggers the analysis for the given Uri
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="locale"></param>
    /// <param name="pages"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Dictionary<string, string?>> AnalyzeDocumentFromUriWithSettingsAsync(Uri uri, string? locale, IList<string> pages, CancellationToken cancellationToken)
    {
        Azure.AnalyzeDocumentOptions? analyzeDocumentOptions = null;
        if (!string.IsNullOrWhiteSpace(locale) && pages.Any())
        {
            analyzeDocumentOptions = new Azure.AnalyzeDocumentOptions() { Locale = locale};
            pages.ToList().ForEach(x => analyzeDocumentOptions.Pages.Add(x));
        }
        
        Azure.AnalyzeDocumentOperation operation = await StartAnalyzeDocumentFromUriAsync("prebuilt-document", uri, analyzeDocumentOptions, cancellationToken);
        await operation.WaitForCompletionAsync(cancellationToken);
        return operation.Value.KeyValuePairs
                   ?.ToDictionary(x => x.Key.Content, x => x.Value?.Content)
               ?? new Dictionary<string, string?>();

    }
    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Dictionary<string, string?>> AnalyzeDocumentFromStreamAsync(Stream stream, CancellationToken cancellationToken)
    {
        return AnalyzeDocumentFromStreamAsync(stream, cancellationToken, null, new List<string>());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="locale"></param>
    /// <param name="pages"></param>
    /// <returns></returns>
    public async Task<Dictionary<string, string?>> AnalyzeDocumentFromStreamAsync(Stream stream, CancellationToken cancellationToken, string? locale, IList<string> pages)
    {
        Azure.AnalyzeDocumentOptions? analyzeDocumentOptions = null;
        if (!string.IsNullOrWhiteSpace(locale) && pages.Any())
        {
            analyzeDocumentOptions = new Azure.AnalyzeDocumentOptions() { Locale = locale};
            pages.ToList().ForEach(x => analyzeDocumentOptions.Pages.Add(x));
        }
            
        Azure.AnalyzeDocumentOperation operation = await StartAnalyzeDocumentAsync("prebuilt-document", stream, 
            analyzeDocumentOptions, 
            cancellationToken : cancellationToken);
        await operation.WaitForCompletionAsync(cancellationToken);
        return operation.Value.KeyValuePairs
                   ?.ToDictionary(x => x.Key.Content, x => x.Value?.Content)
               ?? new Dictionary<string, string?>();

    }
}