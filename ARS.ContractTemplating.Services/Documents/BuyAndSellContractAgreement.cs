using System.Diagnostics.CodeAnalysis;
using ARS.ContractTemplating.Domain.Models.Documents;
using ARS.ContractTemplating.Domain.Models.Persona;
using ARS.ContractTemplating.Services.Documents.Components;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ARS.ContractTemplating.Services.Documents;

/// <summary>
/// Docuent generator for ContractAgreement
/// </summary>
[ExcludeFromCodeCoverage]
public class BuyAndSellContractAgreement : IDocument
{
    private readonly BuyAndSell _model;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="model"></param>
    public BuyAndSellContractAgreement(BuyAndSell model)
    {
        _model = model;
    }

    /// <summary>
    /// Get Metadata
    /// </summary>
    /// <returns></returns>
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    /// <summary>
    /// Compose component
    /// </summary>
    /// <param name="container"></param>
    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);

                page.Footer().AlignCenter().Text(text =>
                {
                    text.CurrentPageNumber();
                    text.Span(" / ");
                    text.TotalPages();
                });
            });
    }

    void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column
                    .Item().Text($"Amount ${_model.Amount}")
                    .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                column.Item().Text(text =>
                {
                    text.Span("Date: ").SemiBold();
                    text.Span($"{DateTime.Today:d}");
                });
            });

            row.ConstantItem(100).Height(50).Placeholder();
        });
    }

    void ComposeContent(IContainer container)
    {
        container.PaddingVertical(40).Column(column =>
        {
            column.Spacing(20);

            column.Item().Row(row =>
            {
                row.RelativeItem().Component(new ContractPersona<Buyer>(_model.Buyer));
                row.ConstantItem(50);
                row.RelativeItem().Component(new ContractPersona<Seller>(_model.Seller));
            });

            //column.Item().Element(ComposeTable);

            if (_model.AdditionalClauses.Any())
                column.Item().PaddingTop(25).Element(ComposeComments);
        });
    }

    /*void ComposeTable(IContainer container)
    {
        var headerStyle = TextStyle.Default.SemiBold();

        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(25);
                columns.RelativeColumn(3);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            table.Header(header =>
            {
                header.Cell().Text("#");
                header.Cell().Text("Product").Style(headerStyle);
                header.Cell().AlignRight().Text("Unit price").Style(headerStyle);
                header.Cell().AlignRight().Text("Quantity").Style(headerStyle);
                header.Cell().AlignRight().Text("Total").Style(headerStyle);

                header.Cell().ColumnSpan(5).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
            });

            foreach (var item in Model.Items)
            {
                table.Cell().Element(CellStyle).Text(Model.Items.IndexOf(item) + 1);
                table.Cell().Element(CellStyle).Text(item.Name);
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Price}$");
                table.Cell().Element(CellStyle).AlignRight().Text(item.Quantity);
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Price * item.Quantity}$");

                static IContainer CellStyle(IContainer container) =>
                    container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
            }
        });
    }*/

    void ComposeComments(IContainer container)
    {
        foreach (var clause in _model.AdditionalClauses)
        {
            container.ShowEntire().Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
            {
                column.Spacing(5);
                column.Item().Text("Comments").FontSize(14).SemiBold();
                column.Item().Text(clause);
            });
        }
        
    }
}