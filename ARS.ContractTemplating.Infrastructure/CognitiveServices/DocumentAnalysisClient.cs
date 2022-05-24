using System.Diagnostics.CodeAnalysis;
using ARS.ContractTemplating.Domain.Interfaces.Infrastructure;
using Azure;
using Azure.Core;

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
    /// <returns></returns>
    public async Task<Dictionary<string, string?>> AnalyzeDocumentFromUriAsync(string url)
    {
        Uri fileUri = new Uri (url);
        Azure.AnalyzeDocumentOperation operation = await StartAnalyzeDocumentFromUriAsync("prebuilt-read", fileUri);
        await operation.WaitForCompletionAsync();
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
    public async Task<Dictionary<string, string?>> AnalyzeDocumentFromStreamAsync(Stream stream, CancellationToken cancellationToken)
    {
        Azure.AnalyzeDocumentOperation operation = await StartAnalyzeDocumentAsync("prebuilt-read", stream, cancellationToken : cancellationToken);
        await operation.WaitForCompletionAsync(cancellationToken);
        return operation.Value.KeyValuePairs
                   ?.ToDictionary(x => x.Key.Content, x => x.Value?.Content)
               ?? new Dictionary<string, string?>();

    }
}