using ARS.ContractTemplating.Domain.Interfaces.Infrastructure;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;

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
    /// Upload provided file as stream to storage and returns the blob url
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="blobName"></param>
    /// <param name="stream"></param>
    /// <param name="cancellationToken"></param>
    public async Task<string> UploadBlobAsync(string containerName, string blobName, MemoryStream stream,
        CancellationToken cancellationToken)
    {
        BlobContainerClient containerClient = GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        var blobClient = containerClient.GetBlobClient(blobName);
        await blobClient.UploadAsync(stream, cancellationToken: cancellationToken);
        return blobClient.Uri.AbsoluteUri;
    }

    /// <summary>
    /// Download blob to stream
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="blobName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Stream> DownloadBlobsAsStreamAsync(string containerName, string blobName, CancellationToken cancellationToken)
    {
        BlobContainerClient containerClient = GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);
        var stream = new MemoryStream();
        await blobClient.DownloadToAsync(stream, cancellationToken);
        return stream;
    }

    /// <summary>
    /// Gets a SAS with 5 min lifespan to the provided blob
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="blobName"></param>
    /// <returns></returns>
    public Uri GetBlobSasUrl(string containerName, string blobName)
    {
        BlobContainerClient containerClient = GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);
        return blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow + TimeSpan.FromSeconds(300));
    }
}