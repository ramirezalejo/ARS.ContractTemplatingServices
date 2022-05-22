using ARS.ContractTemplating.Domain.Contracts;
using Azure.Storage.Blobs;

namespace ARS.ContractTemplating.Infrastructure.Blobs;

/// <summary>
/// Wrapper for Azure SDK
/// </summary>
public class BlobClient : BlobContainerClient, IBlobClient
{
    /// <summary>
    /// Constructor matching the base type
    /// </summary>
    /// <param name="connectionString"></param>
    /// <param name="blobContainerName"></param>
    public BlobClient(string connectionString, string blobContainerName) : base(connectionString, blobContainerName)
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
        BlobContainerClient containerClient = await GetParentBlobServiceClientCore()
            .CreateBlobContainerAsync(containerName, cancellationToken: cancellationToken);
        await containerClient.UploadBlobAsync(blobName, stream, cancellationToken);
    }
}