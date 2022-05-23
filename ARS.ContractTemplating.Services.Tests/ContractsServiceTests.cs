using System.Diagnostics;
using ARS.ContractTemplating.Domain.Contracts.Infrastructure;
using ARS.ContractTemplating.Domain.Models.Messages;
using AutoFixture;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace ARS.ContractTemplating.Services.Tests;
[TestFixture]
public class ContractsServiceTests
{
    private ICognitiveServicesClient? _cognitiveServicesClient;
    private ILogger? _logger;
    private ContractRequestMessage? _requestMessage;
    private Fixture? _fixture;
    
    //Setup
    [SetUp]
    public void Setup()
    {
        _cognitiveServicesClient = Substitute.For<ICognitiveServicesClient>();
        _logger = Substitute.For<ILogger>();
        _fixture = new Fixture();
        _requestMessage = _fixture.Create<ContractRequestMessage>();
    }
    
    
    [Test]
    public async Task GenerateContracts_Succeeded()
    {
        //Arrange
        Debug.Assert(_cognitiveServicesClient != null, nameof(_cognitiveServicesClient) + " != null");
        Debug.Assert(_logger != null, nameof(_logger) + " != null");
        var contractsService = new ContractsService(_cognitiveServicesClient, _logger);
        _cognitiveServicesClient.ReadFileUrl(Arg.Any<string>()).Returns(Task.FromResult(new List<string>()));
        //Act
        Debug.Assert(_requestMessage != null, nameof(_requestMessage) + " != null");
        await contractsService.GenerateContract(_requestMessage);

        //Assert
        Assert.Pass();
    }
}