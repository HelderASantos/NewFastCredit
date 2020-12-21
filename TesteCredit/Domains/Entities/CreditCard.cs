using System;
namespace TesteCredit.Domains.Entities
{
    public class CreditCard
    {
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public long SecurityCode { get; set; }
        public double PurchaseLimit { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
