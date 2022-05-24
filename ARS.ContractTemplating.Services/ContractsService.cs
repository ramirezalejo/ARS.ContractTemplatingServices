using ARS.ContractTemplating.Domain.Enums;
using ARS.ContractTemplating.Domain.Interfaces.Infrastructure;
using ARS.ContractTemplating.Domain.Interfaces.Services;
using ARS.ContractTemplating.Domain.Models.Documents;
using ARS.ContractTemplating.Domain.Models.Messages;
using ARS.ContractTemplating.Domain.Models.Persona;
using ARS.ContractTemplating.Services.Documents;
using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;

namespace ARS.ContractTemplating.Services;

/// <summary>
/// Contracts Service
/// </summary>
public class ContractsService : IContractsService
{
    private readonly IDocumentAnalysisClient _documentAnalysisClient;
    private readonly IBlobClient _blobClient;
    private readonly ILogger _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="documentAnalysisClient"></param>
    /// <param name="blobClient"></param>
    /// <param name="logger"></param>
    public ContractsService(
        IDocumentAnalysisClient documentAnalysisClient,
        IBlobClient blobClient,
        ILogger<ContractsService> logger)
    {
        _documentAnalysisClient = documentAnalysisClient;
        _blobClient = blobClient;
        _logger = logger;
    }

    /// <summary>
    /// Generates the contract and uploads it to the storage
    /// </summary>
    /// <param name="requestMessage"></param>
    /// <param name="cancellationToken"></param>
    public async Task GenerateContract(ContractRequestMessage requestMessage, CancellationToken cancellationToken)
    {
        var buyerFile = requestMessage.Files?.FirstOrDefault(x => x.FileRoleType == FileRoleType.Seller);
        try
        {
            Dictionary<string, string?> buyerDataFromFile;
            if (buyerFile != null && !string.IsNullOrWhiteSpace(buyerFile.ContainerName) && !string.IsNullOrWhiteSpace(buyerFile.BlobName))
            {
                buyerDataFromFile = await _documentAnalysisClient.AnalyzeDocumentFromStreamAsync(
                    await _blobClient.DownloadBlobsAsStreamAsync(buyerFile.ContainerName, buyerFile.BlobName),
                    cancellationToken);
            }
            else
            {
                buyerDataFromFile = buyerFile is not null
                    ? await _documentAnalysisClient.AnalyzeDocumentFromUriAsync(buyerFile.FilePath ??
                        throw new InvalidOperationException())
                    : new Dictionary<string, string?>();
            }
            buyerDataFromFile.TryGetValue("NOMBRES", out var firstName);
            buyerDataFromFile.TryGetValue("APELLIDOS", out var lastName);
            buyerDataFromFile.TryGetValue("CEDULA DE CIUDADANIA", out var identification);
            var buyer = new Buyer($"{firstName} {lastName}",
                "CC",
                identification ?? "1234",
                new Address()
            );

            var seller = new Seller($"{firstName} {lastName}",
                "CC",
                identification ?? "1234",
                new Address()
            );
            var model = new BuyAndSell(buyer, seller, new string[0], 0);
            var doc = new BuyAndSellContractAgreement(model).GeneratePdf();

            using MemoryStream stream = new MemoryStream(doc);
            await _blobClient.UploadBlobAsync("result", "Contrato1", stream, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error processing contract request");
            throw;
        }
        
    }
}