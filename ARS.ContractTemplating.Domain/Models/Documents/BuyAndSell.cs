using ARS.ContractTemplating.Domain.Models.Persona;

namespace ARS.ContractTemplating.Domain.Models.Documents;
/// <summary>
/// Model for data related to But and Sell document
/// </summary>
public class BuyAndSell
{
    /// <summary>
    /// Buy And Sell document model constructor
    /// </summary>
    /// <param name="buyer"></param>
    /// <param name="seller"></param>
    /// <param name="additionalClauses"></param>
    /// <param name="amount"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public BuyAndSell(Buyer buyer, Seller seller, string[] additionalClauses, decimal amount)
    {
        Buyer = buyer ?? throw new ArgumentNullException(nameof(buyer));
        Seller = seller ?? throw new ArgumentNullException(nameof(seller));
        AdditionalClauses = additionalClauses;
        Amount = amount;
    }

    /// <summary>
    /// Buyer
    /// </summary>
    public Buyer Buyer { get; set; }
    /// <summary>
    /// Seller
    /// </summary>
    public Seller Seller { get; set; }

    /// <summary>
    /// Additional clauses
    /// </summary>
    public string[] AdditionalClauses { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    public decimal Amount { get; set; }
}