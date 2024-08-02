using Microsoft.AspNetCore.Mvc;
using TryQuestPDF.Models;

namespace TryQuestPDF.Controllers
{
    public class ReportController : Controller
    {
        private readonly PdfService _pdfService;

        public ReportController(PdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public IActionResult GenerateInvoice()
        {
            var model = new InvoiceModel
            {
                CompanyName = "COMPANY",
                InvoiceTo = "NAME SURNAME\nCreative Manager\nPhone: 00 123 456 789",
                PaymentInfo = "Payment Info:\nBank Name: ABCD\nAccount Number: 123456789",
                Date = DateTime.Now.ToString("dd/MM/yyyy"),
                Items = new List<InvoiceItem>
            {
                new InvoiceItem { Description = "Product Name/Item 1", Quantity = 1, Price = 100 },
                new InvoiceItem { Description = "Product Name/Item 2", Quantity = 1, Price = 200 },
                // Add more items as needed
            },
                SubTotal = 300,
                Tax = 30,
                Total = 330
            };

            var pdf = _pdfService.GenerateInvoicePdf(model);
            return File(pdf, "application/pdf", "Invoice.pdf");
        }

        public IActionResult PDFInvoice()
        {
            var model = new InvoiceModel
            {
                CompanyName = "COMPANY",
                InvoiceTo = "NAME SURNAME\nCreative Manager\nPhone: 00 123 456 789",
                PaymentInfo = "Payment Info:\nBank Name: ABCD\nAccount Number: 123456789",
                Date = DateTime.Now.ToString("dd/MM/yyyy"),
                Items = new List<InvoiceItem>
            {
                new InvoiceItem { Description = "Product Name/Item 1", Quantity = 1, Price = 100 },
                new InvoiceItem { Description = "Product Name/Item 2", Quantity = 1, Price = 200 },
                // Add more items as needed
            },
                SubTotal = 300,
                Tax = 30,
                Total = 330
            };


            return View(model);
        }
    }
}