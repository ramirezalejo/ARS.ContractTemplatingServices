using System.Diagnostics.CodeAnalysis;

namespace ARS.ContractTemplating.Domain.Models.Persona;
/// <summary>
/// Buyer Model
/// </summary>
[ExcludeFromCodeCoverage]
public class Buyer : PersonaBase
{
    /// <summary>
    /// Buyer constructor
    /// </summary>
    /// <param name="fullName"></param>
    /// <param name="identificationType"></param>
    /// <param name="identification"></param>
    /// <param name="address"></param>
    public Buyer(string fullName, string identificationType, string identification, Address address) : base(fullName, identificationType, identification, address)
    {
    }
}