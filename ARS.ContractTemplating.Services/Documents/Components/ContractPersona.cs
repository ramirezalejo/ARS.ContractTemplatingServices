using System.Diagnostics.CodeAnalysis;
using ARS.ContractTemplating.Domain.Models.Persona;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ARS.ContractTemplating.Services.Documents.Components;

/// <summary>
/// Contract persona document component
/// </summary>
[ExcludeFromCodeCoverage]
public class ContractPersona<T> : IComponent  where T : PersonaBase
{
    private readonly T _model;

    /// <summary>
    /// Generic Constructor
    /// </summary>
    public ContractPersona(T model)
    {
        _model = model;
    }
    
    /// <summary>
    /// Compose document section from model
    /// </summary>
    /// <param name="container"></param>
    public void Compose(IContainer container)
    {
        container.ShowEntire().Column(column =>
        {
            column.Spacing(2);

            column.Item().Text(_model.FullName).SemiBold();
            column.Item().PaddingBottom(5).LineHorizontal(1); 
            
            column.Item().Text(_model.IdentificationType);    
            column.Item().Text(_model.Identification);
            column.Item().Text(_model.Address.City);
            column.Item().Text(_model.Address.Street);
        });
    }
}