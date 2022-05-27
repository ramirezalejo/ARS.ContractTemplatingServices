using System.Diagnostics.CodeAnalysis;

namespace ARS.ContractTemplating.Domain.Models.Persona;
/// <summary>
/// Base properties for Persona derived models
/// </summary>
[ExcludeFromCodeCoverage]
public abstract class PersonaBase
{
    /// <summary>
    /// PersonaBase constructor
    /// </summary>
    /// <param name="fullName"></param>
    /// <param name="identificationType"></param>
    /// <param name="identification"></param>
    /// <param name="address"></param>
    protected PersonaBase(string fullName, string identificationType, string identification, Address address)
    {
        FullName = fullName;
        IdentificationType = identificationType;
        Identification = identification;
        Address = address;
    }

    /// <summary>
    /// Full Name
    /// </summary>
    public string FullName { get; set; }
    /// <summary>
    /// Identification type
    /// </summary>
    public string IdentificationType { get; set; }
    /// <summary>
    /// Identification Id
    /// </summary>
    public string Identification { get; set; }

    /// <summary>
    /// Address
    /// </summary>
    public Address Address { get; set; }

}