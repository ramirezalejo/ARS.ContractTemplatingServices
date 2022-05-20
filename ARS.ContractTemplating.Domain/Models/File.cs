using System.Text.Json.Serialization;

namespace ARS.ContractTemplating.Domain.Models;

/// <summary>
/// Entity for the Uploaded Files
/// </summary>
public class File
{
    /// <summary>
    /// Actual file data
    /// </summary>
    public string? Base64Data { get; set; }
    /// <summary>
    /// Content type
    /// </summary>
    public string? ContentType { get; set; }
    /// <summary>
    /// File Name
    /// </summary>
    public string? FileName { get; set; }
    /// <summary>
    /// User that uploaded the file
    /// </summary>
    public string? Owner { get; set; }
    /// <summary>
    /// File state, for compression checks (compress or not)
    /// </summary>
    public byte State { get; set; }

    /// <summary>
    /// Buffer used while processing the file
    /// </summary>
    [JsonIgnore]
    public byte[]? Buffer { get; set; }
    /// <summary>
    /// Original file size
    /// </summary>
    [JsonIgnore]
    public int OriginalSize { get; set; }
    /// <summary>
    /// File size when compressed
    /// </summary>
    [JsonIgnore]
    public int CompressedSize { get; set; }
/// <summary>
/// Validation for type of content to check if is a valid file
/// </summary>
/// <returns>bool</returns>
    public bool IsOkToCompress()
    {
        if (ContentType != null)
        {
            var low = ContentType.ToLower();
            if (low.StartsWith("image/"))
                return low.EndsWith("/svg+xml") || low.EndsWith("/bmp");
            if (low.StartsWith("audio/") || low.StartsWith("video/"))
                return low.EndsWith("/wav");
            if (low.StartsWith("text/"))
                return true;
            if(low.StartsWith("application/"))
            {
                switch(low.Split('/')[1])
                {
                    case "x-abiword":
                    case "octet-stream": // assume it can be compressed
                    case "x-csh":
                    case "x-msword":
                    case "vnd.openxmlformats-officedocument.wordprocessingml.document":
                    case "json":
                    case "ld+json":
                    case "vnd.apple.installer+xml":
                    case "vnd.oasis.opendocument.presentation":
                    case "vnd.oasis.opendocument.spreadsheet":
                    case "vnd.oasis.opendocument.text":
                    case "x-httpd-php":
                    case "vnd.ms-powerpoint":
                    case "vnd.openxmlformats-officedocument.presentationml.presentation":
                    case "rtf":
                    case "x-sh":
                    case "vnd.visio":
                    case "xhtml+xml":
                    case "vnd.ms-excel":
                    case "vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    case "xml":
                    case "vnd.mozilla.xul+xml":
                        return true;
                }
            }
        }
        return false;
    }
}