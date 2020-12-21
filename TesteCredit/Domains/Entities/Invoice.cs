using System;
namespace TesteCredit.Domains.Entities
{
    public class Invoice
    {
        public Purchases Purchase { get; set; }
        public DateTime PaymentDate { get; set; }
        public double TotalInvoice { get; set; }
    }

    public class Purchases
    {
        public DateTime PurchaseDate { get; set; }
        public string DescriptionPurchase { get; set; }
        public double Spent { get; set; }
    }
}
