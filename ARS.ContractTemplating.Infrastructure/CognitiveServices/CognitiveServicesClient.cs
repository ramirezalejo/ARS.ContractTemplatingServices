using ARS.ContractTemplating.Domain.Contracts;
using ARS.ContractTemplating.Domain.Contracts.Infrastructure;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Logging;

namespace ARS.ContractTemplating.Infrastructure.CognitiveServices;
/// <summary>
/// Client to integrate with cognitive services
/// </summary>
public class CognitiveServicesClient : ComputerVisionClient, ICognitiveServicesClient
{
    private readonly ILogger _logger;

    /// <summary>
    /// Constructor to create the required client
    /// </summary>
    /// <param name="httpClientFactory"></param>
    /// <param name="endpoint"></param>
    /// <param name="key"></param>
    /// <param name="logger"></param>
    public CognitiveServicesClient(IHttpClientFactory httpClientFactory, string endpoint, string key, ILogger logger) : base(new ApiKeyServiceClientCredentials(key), httpClientFactory.CreateClient(), false)
    {
        _logger = logger;
        Endpoint = endpoint;
    }

    /// <summary>
    /// Get the text out of an image provided the url of the image
    /// </summary>
    /// <param name="sourceFileUrl"></param>
    public async Task<List<string>> ReadFileUrl(string sourceFileUrl)
    {
        // Read text from URL
        var textHeaders = await this.ReadAsync(sourceFileUrl);
        // After the request, get the operation location (operation ID)
        string operationLocation = textHeaders.OperationLocation;
        //await Task.Delay(2000);

        // Retrieve the URI where the extracted text will be stored from the Operation-Location header.
        // We only need the ID and not the full URL
        const int numberOfCharsInOperationId = 36;
        string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);

        // Extract the text
        ReadOperationResult results;
        _logger.LogInformation("Extracting text from URL file {SourceFileUrl}...", sourceFileUrl);
        do
        {
            results = await this.GetReadResultAsync(Guid.Parse(operationId));
        }
        while ((results.Status == OperationStatusCodes.Running ||
                results.Status == OperationStatusCodes.NotStarted));

        // Display the found text.
        var textUrlFileResults = results.AnalyzeResult.ReadResults;
        return textUrlFileResults.SelectMany(x => x.Lines.Select(y => y.Text)).ToList();
    }
}