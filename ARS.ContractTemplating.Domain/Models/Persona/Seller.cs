using System.Diagnostics.CodeAnalysis;

namespace ARS.ContractTemplating.Domain.Models.Persona;
/// <summary>
/// Seller Model
/// </summary>
[ExcludeFromCodeCoverage]
public class Seller : PersonaBase
{
    /// <summary>
    /// Seller constructor
    /// </summary>
    /// <param name="fullName"></param>
    /// <param name="identificationType"></param>
    /// <param name="identification"></param>
    /// <param name="address"></param>
    public Seller(string fullName, string identificationType, string identification, Address address) : base(fullName, identificationType, identification, address)
    {
    }
}