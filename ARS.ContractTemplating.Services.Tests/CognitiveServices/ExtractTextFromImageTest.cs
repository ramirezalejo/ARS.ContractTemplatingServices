using ARS.ContractTemplating.Domain.Contracts;
using ARS.ContractTemplating.Services.CognitiveServices;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace ARS.ContractTemplating.Services.Tests.CognitiveServices;

[TestFixture]
public class ExtractTextFromImageTest
{
    private ILogger? _logger;
    private ICognitiveServicesClient? _cognitiveServicesClient;
    [Test]
    public void ExtractTextFromImage_Constructor_Test()
    {
        _logger = Substitute.For<ILogger>();
        _cognitiveServicesClient = Substitute.For<ICognitiveServicesClient>();
        var extractTextFromImage = new ExtractTextFromImage(_cognitiveServicesClient, _logger);
        Assert.IsNotNull(extractTextFromImage);
    }
}