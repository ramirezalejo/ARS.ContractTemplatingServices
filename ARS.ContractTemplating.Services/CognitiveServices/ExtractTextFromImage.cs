using Microsoft.Extensions.Logging;

namespace ARS.ContractTemplating.Services.CognitiveServices;

/// <summary>
/// Contains the logic to extract text from images
/// </summary>
public class ExtractTextFromImage
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    /// <summary>
    /// Constructor injecting the HttpClient and Logger instances
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="logger"></param>
    public ExtractTextFromImage(HttpClient httpClient, ILogger logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    
}