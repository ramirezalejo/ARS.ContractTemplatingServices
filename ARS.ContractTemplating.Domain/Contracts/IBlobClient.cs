namespace ARS.ContractTemplating.Domain.Contracts;

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
    Task UploadBlobAsync(string containerName, string blobName, MemoryStream stream, CancellationToken cancellationToken);
}