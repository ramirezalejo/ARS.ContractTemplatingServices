using System.Diagnostics;
using ARS.ContractTemplating.Domain.Interfaces.Infrastructure;
using ARS.ContractTemplating.Domain.Models.Messages;
using AutoFixture;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace ARS.ContractTemplating.Services.Tests;

[TestFixture]
public class ContractsServiceTests
{
    //private ICognitiveServicesClient? _cognitiveServicesClient;
    private IDocumentAnalysisClient? _documentAnalysisClient;
    private IBlobClient? _blobClient;
    private ILogger<ContractsService>? _logger;
    private ContractRequestMessage? _requestMessage;
    private Fixture? _fixture;

    //Setup
    [SetUp]
    public void Setup()
    {
        // _cognitiveServicesClient = Substitute.For<ICognitiveServicesClient>();
        _documentAnalysisClient = Substitute.For<IDocumentAnalysisClient>();
        _blobClient = Substitute.For<IBlobClient>();
        _logger = Substitute.For<ILogger<ContractsService>>();
        _fixture = new Fixture();
        _requestMessage = _fixture.Create<ContractRequestMessage>();
    }


    [Test]
    public async Task GenerateContracts__SucceededFromStream()
    {
        //Arrange
        //Debug.Assert(_cognitiveServicesClient != null, nameof(_cognitiveServicesClient) + " != null");
        Debug.Assert(_logger != null, nameof(_logger) + " != null");
        Debug.Assert(_documentAnalysisClient != null, nameof(_documentAnalysisClient) + " != null");
        Debug.Assert(_blobClient != null, nameof(_blobClient) + " != null");
        var contractsService = new ContractsService(_documentAnalysisClient, _blobClient, _logger);
        // _cognitiveServicesClient.ReadFileUrl(Arg.Any<string>()).Returns(Task.FromResult(new List<string>()));
        _documentAnalysisClient.AnalyzeDocumentFromStreamAsync(Arg.Any<Stream>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new Dictionary<string, string?>()));
            
        _blobClient.DownloadBlobsAsStreamAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(Stream.Null));
        //Act
        Debug.Assert(_requestMessage != null, nameof(_requestMessage) + " != null");
        await contractsService.GenerateContract(_requestMessage, default);

        //Assert
        Assert.Pass();
    }
    
    [Test]
        public async Task GenerateContracts__SucceeFromUrlded()
        {
            //Arrange
            foreach (var file in _requestMessage.Files)
            {
                file.ContainerName = null;
            }
            
            //Debug.Assert(_cognitiveServicesClient != null, nameof(_cognitiveServicesClient) + " != null");
            Debug.Assert(_logger != null, nameof(_logger) + " != null");
            Debug.Assert(_documentAnalysisClient != null, nameof(_documentAnalysisClient) + " != null");
            Debug.Assert(_blobClient != null, nameof(_blobClient) + " != null");
            var contractsService = new ContractsService(_documentAnalysisClient, _blobClient, _logger);
            // _cognitiveServicesClient.ReadFileUrl(Arg.Any<string>()).Returns(Task.FromResult(new List<string>()));
            _documentAnalysisClient.AnalyzeDocumentFromUriAsync(Arg.Any<string>())
                .Returns(Task.FromResult(new Dictionary<string, string?>()));
            //Act
            Debug.Assert(_requestMessage != null, nameof(_requestMessage) + " != null");
            await contractsService.GenerateContract(_requestMessage, default);
    
            //Assert
            Assert.Pass();
        }
}