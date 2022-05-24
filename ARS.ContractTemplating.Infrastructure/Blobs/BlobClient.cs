using ARS.ContractTemplating.Domain.Interfaces.Infrastructure;
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
        var blobClient = containerClient.GetBlobClient(blobName);
        await blobClient.UploadAsync(stream, cancellationToken: cancellationToken);
        return blobClient.Uri.AbsoluteUri;
    }

    /// <summary>
    /// Download blob to stream
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="blobName"></param>
    /// <returns></returns>
    public async Task<Stream> DownloadBlobsAsStreamAsync(string containerName, string blobName)
    {
        BlobContainerClient containerClient = GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();
        var blobClient = containerClient.GetBlobClient(blobName);
        var stream = new MemoryStream();
        await blobClient.DownloadToAsync(stream);
        return stream;
    }
}