using ARS.ContractTemplating.Domain.Contracts;
using Microsoft.Extensions.Logging;

namespace ARS.ContractTemplating.Services.CognitiveServices;

/// <summary>
/// Contains the logic to extract text from images
/// </summary>
public class ExtractTextFromImage
{
    private readonly ICognitiveServicesClient _cognitiveServicesClient;
    private readonly ILogger _logger;

    /// <summary>
    /// Constructor injecting the HttpClient and Logger instances
    /// </summary>
    /// <param name="cognitiveServicesClient"></param>
    /// <param name="logger"></param>
    public ExtractTextFromImage(ICognitiveServicesClient cognitiveServicesClient, ILogger logger)
    {
        _cognitiveServicesClient = cognitiveServicesClient;
        _logger = logger;
    }
    
    
}