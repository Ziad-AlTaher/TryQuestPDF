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

                // Add watermark in the background
                //page.Background().Element(container => ComposeWatermark(container, webRootPath));

                page.Header().Element(container => ComposeHeader(container, webRootPath));
                page.Content().Element(x => ComposeContent(x, model));
                page.Footer().Element(ComposeFooter);
            });
        });

        return document.GeneratePdf();
    }

    void ComposeWatermark(QuestPDF.Infrastructure.IContainer container, string webRootPath)
    {
        container.AlignCenter().AlignMiddle().Image($"{webRootPath}/Images/watermark.png", ImageScaling.FitArea);
    }

    void ComposeHeader(QuestPDF.Infrastructure.IContainer container, string webRootPath)
    {
        container.Row(row =>
        {
            
            // Add a column for the blue line extending to the far left
            row.ConstantColumn(5).Background(Colors.Blue.Medium);

            // Add a column for the company name with padding
            row.RelativeColumn().Element(innerContainer =>
            {
                innerContainer.PaddingLeft(5).Text("COMPANY\nYour Slogan")
                    .FontSize(20)
                    .SemiBold();
            });

            // Add a column for the company logo
            row.ConstantColumn(100).Image($"{webRootPath}/Images/OIP (1).jpg");
        });
        //container.Row(row =>
        //{
        //    row.ConstantColumn(50).Background(Colors.Blue.Medium);
        //});
    }
    void ComposeHeader2(QuestPDF.Infrastructure.IContainer container, string webRootPath)
    {
        //container.Row(row =>
        //{
            
        //    // Add a column for the blue line extending to the far left
        //    row.ConstantColumn(5).Background(Colors.Blue.Medium);

        //    // Add a column for the company name with padding
        //    row.RelativeColumn().Element(innerContainer =>
        //    {
        //        innerContainer.PaddingLeft(5).Text("COMPANY\nYour Slogan")
        //            .FontSize(20)
        //            .SemiBold();
        //    });

        //    // Add a column for the company logo
        //    row.ConstantColumn(100).Image($"{webRootPath}/Images/OIP (1).jpg");
        //});
        container.Row(row =>
        {
            row.ConstantColumn(50).Background(Colors.Blue.Medium);
        });
    }

    void ComposeContent(QuestPDF.Infrastructure.IContainer container, InvoiceModel model)
    {
        // Implement the content composition logic here
        container.Column(column =>
        {
            column.Item().Text($"Invoice Number: Id");
            column.Item().Text($"Date: {model.Date}");
            // Add more content as needed
        });
    }

    void ComposeFooter(QuestPDF.Infrastructure.IContainer container)
    {
        // Implement the footer composition logic here
        container.AlignCenter().Text("Thank you for your business!");
    }

}