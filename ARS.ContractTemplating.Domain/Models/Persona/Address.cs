using System.Diagnostics.CodeAnalysis;

namespace ARS.ContractTemplating.Domain.Models.Persona;

/// <summary>
/// Address model
/// </summary>
[ExcludeFromCodeCoverage]
public class Address
{
    /// <summary>
    /// Country
    /// </summary>
    public string? Country { get; set; }
    /// <summary>
    /// City
    /// </summary>
    public string? City { get; set; }
    /// <summary>
    /// Address street
    /// </summary>
    public string? Street { get; set; }
}