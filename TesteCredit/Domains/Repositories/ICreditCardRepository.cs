using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteCredit.Domains.Entities;

namespace TesteCredit.Domains.Repositories
{
    public interface ICreditCardRepository
    {
        Task<IEnumerable<Purchases>> GetInvoiceByPeriodAsync(string id, DateTime startAt, DateTime finishAt);
        Task<IEnumerable<Purchases>> GetPurchasesByPeriodAsync(string id, DateTime? startAt, DateTime? finishAt, DateTime? referenceDate);
        Task<IEnumerable<PaymentHistory>> GetPaymentHistoryAsync(string id);
        void SendInvoice(string clientId);
    }
}