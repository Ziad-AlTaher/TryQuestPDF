namespace TryQuestPDF.Models
{
    public class InvoiceModel
    {
        public string CompanyName { get; set; }
        public string InvoiceTo { get; set; }
        public string PaymentInfo { get; set; }
        public string Date { get; set; }
        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }

    public class InvoiceItem
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
