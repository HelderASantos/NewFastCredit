using System;
namespace TesteCredit.Domains.Entities
{
    public class PaymentHistory
    {
        public string Name { get; set; }
        public string ReferenceMonth { get; set; }
        public string Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public double PaidOut { get; set; }
    }
}
