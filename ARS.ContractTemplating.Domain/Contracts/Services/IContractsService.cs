using ARS.ContractTemplating.Domain.Models.Messages;

namespace ARS.ContractTemplating.Domain.Contracts.Services;
/// <summary>
/// Contracts for ContractService
/// </summary>
public interface IContractsService
{
    /// <summary>
    /// Generates the contract pdf based on the request
    /// </summary>
    /// <param name="requestMessage"></param>
    /// <returns></returns>
    Task GenerateContract(ContractRequestMessage requestMessage);
}