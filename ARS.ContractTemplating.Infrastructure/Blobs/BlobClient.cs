using ARS.ContractTemplating.Domain.Contracts.Infrastructure;
using Azure.Storage.Blobs;

namespace ARS.ContractTemplating.Infrastructure.Blobs;

/// <summary>
/// Wrapper for Azure SDK
/// </summary>
public class BlobClient : BlobServiceClient, IBlobClient
{
    /// <summary>
    /// Constructor matching the base type
    /// </summary>
    /// <param name="connectionString"></param>
    public BlobClient(string connectionString) : base(connectionString)
    {
    }

    /// <summary>
    /// Upload provided file as stream to storage
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="blobName"></param>
    /// <param name="stream"></param>
    /// <param name="cancellationToken"></param>
    public async Task UploadBlobAsync(string containerName, string blobName, MemoryStream stream,
        CancellationToken cancellationToken)
    {
        BlobContainerClient containerClient = GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        await containerClient.UploadBlobAsync(blobName, stream, cancellationToken);
    }
}