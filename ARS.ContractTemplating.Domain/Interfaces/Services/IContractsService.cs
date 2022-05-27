using ARS.ContractTemplating.Domain.Models.Messages;

namespace ARS.ContractTemplating.Domain.Interfaces.Services;
/// <summary>
/// Contracts for ContractService
/// </summary>
public interface IContractsService
{
    /// <summary>
    /// Generates the contract pdf based on the request
    /// </summary>
    /// <param name="requestMessage"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task GenerateContract(ContractRequestMessage requestMessage, CancellationToken cancellationToken);
}