using System.Diagnostics.CodeAnalysis;
using ARS.ContractTemplating.Domain.Enums;

namespace ARS.ContractTemplating.Domain.Models.Messages;
/// <summary>
/// Model for Contract Request Files
/// </summary>
[ExcludeFromCodeCoverage]
public class ContractRequestFile
{
    /// <summary>
    /// File Location
    /// </summary>
    public string? FilePath { get; set; }

    /// <summary>
    /// RoleType of file
    /// </summary>
    public FileRoleType FileRoleType { get; set; }
    
    /// <summary>
    /// Blob Container name
    /// </summary>
    public string? ContainerName { get; set; }

    /// <summary>
    /// Blob Name
    /// </summary>
    public string? BlobName { get; set; }
    
}