namespace ARS.ContractTemplating.Domain.Models.Messages;
/// <summary>
/// Model for ContractRequest Queue and Interactions
/// </summary>
public class ContractRequestMessage
{
    /// <summary>
    /// List of file to use for contract
    /// </summary>
    public ContractRequestFile[]? Files { get; set; }

    /// <summary>
    /// Contract Requestor/ Request Owner
    /// </summary>
    public string? Owner { get; set; }

    /// <summary>
    /// Date of the request
    /// </summary>
    public DateTime RequestDate { get; set; }

    /// <summary>
    /// Contract template Id
    /// </summary>
    public string? TemplateId { get; set; }
}