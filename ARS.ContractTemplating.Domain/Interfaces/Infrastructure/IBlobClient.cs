namespace ARS.ContractTemplating.Domain.Interfaces.Infrastructure;

/// <summary>
/// Interface for Blob interactions
/// </summary>
public interface IBlobClient
{
    /// <summary>
    /// Uploads the blob provided as stream to the storage
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="blobName"></param>
    /// <param name="stream"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> UploadBlobAsync(string containerName, string blobName, MemoryStream stream, CancellationToken cancellationToken);

    /// <summary>
    /// Download blob to stream
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="blobName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Stream> DownloadBlobsAsStreamAsync(string containerName, string blobName, CancellationToken cancellationToken);
}