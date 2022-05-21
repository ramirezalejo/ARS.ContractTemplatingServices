using ARS.ContractTemplating.Services.CognitiveServices;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace ARS.ContractTemplating.Services.Tests.CognitiveServices;

[TestFixture]
public class ExtractTextFromImageTest
{
    private ILogger _logger;
    private HttpClient _httpClient;
    [Test]
    public void ExtractTextFromImage_Constructor_Test()
    {
        _logger = Substitute.For<ILogger>();
        _httpClient = Substitute.For<HttpClient>();
        var extractTextFromImage = new ExtractTextFromImage(_httpClient, _logger);
        Assert.IsNotNull(extractTextFromImage);
    }
}