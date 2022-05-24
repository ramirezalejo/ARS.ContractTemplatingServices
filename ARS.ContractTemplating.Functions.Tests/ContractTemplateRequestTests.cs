using System.Text.Json;
using ARS.ContractTemplating.Domain.Interfaces.Services;
using ARS.ContractTemplating.Domain.Models.Messages;
using AutoFixture;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace ARS.ContractTemplating.Functions.Tests;
[TestFixture]
public class ContractTemplateRequestTests
{
    private IContractsService? _contractService;
    private ILogger? _logger;
    private Fixture? _fixture;
    private string? _requestMessage;

    //setup
    [SetUp]
    public void Setup()
    {
        _contractService = Substitute.For<IContractsService>();
        _logger = Substitute.For<ILogger>();
        _fixture = new Fixture();
        _requestMessage = JsonSerializer.Serialize(_fixture.Create<ContractRequestMessage>());
    }

    //Test RunAsync
    [Test]
    public async Task RunAsync_Succeeded()
    {
        //Arrange
        var contractTemplateRequest = new ContractTemplateRequest(_contractService, _logger);
        //Act
        await contractTemplateRequest.RunAsync(_requestMessage, default);
        //Assert
        Assert.Pass();
    }
}