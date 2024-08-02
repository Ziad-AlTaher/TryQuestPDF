using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel;
using TryQuestPDF.Models;

public class PdfService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public PdfService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public byte[] GenerateInvoicePdf(InvoiceModel model)
    {
        string webRootPath = _webHostEnvironment.WebRootPath;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.Header().Element(container => ComposeHeader(container, webRootPath)); // Pass webRootPath here
                page.Content().Element(x => ComposeContent(x, model));
                page.Footer().Element(ComposeFooter);
            });
        });

        return document.GeneratePdf();
    }

    void ComposeHeader(QuestPDF.Infrastructure.IContainer container, string webRootPath)
    {
        container.Row(row =>
        {
            row.RelativeColumn().Text("COMPANY\nYour Slogan").FontSize(20).SemiBold();
            row.ConstantColumn(100).Image($"{webRootPath}/Images/OIP (1).jpg");
        });
    }

    void ComposeContent(QuestPDF.Infrastructure.IContainer container, InvoiceModel model)
    {
        container.PaddingVertical(10).Column(column =>
        {
            column.Item().Row(row =>
            {
                row.RelativeColumn().Text($"Invoice To:\n{model.InvoiceTo}");
                row.ConstantColumn(100).Text($"Date:\n{model.Date}");
            });

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.ConstantColumn(50);
                    columns.ConstantColumn(50);
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle);
                    header.Cell().Element(CellStyle);
                    header.Cell().Element(CellStyle);
                });

                foreach (var item in model.Items)
                {
                    table.Cell().Element(CellStyle).Text(item.Description);
                    table.Cell().Element(CellStyle).Text(item.Quantity.ToString());
                    table.Cell().Element(CellStyle).Text(item.Price.ToString("C"));
                }
            });

            column.Item().PaddingTop(10).AlignRight().Text($"Subtotal: {model.SubTotal:C}");
            column.Item().AlignRight().Text($"Tax: {model.Tax:C}");
            column.Item().AlignRight().Text($"Total: {model.Total:C}");
        });
    }

    void ComposeFooter(QuestPDF.Infrastructure.IContainer container)
    {
        container.AlignCenter().Text("Thank you for your business!");
    }

    QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container)
    {
        return container.Border(1).Padding(5);
    }
}