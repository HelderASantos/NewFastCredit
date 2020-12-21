using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using TesteCredit.Domains.Entities;
using TesteCredit.Domains.Repositories;

namespace TesteCredit.Infra.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly string _connectionString;

        public CreditCardRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Purchases>> GetInvoiceByPeriodAsync(string id, DateTime startAt, DateTime finishAt)
        {
            var query = $@"SELECT PurchaseDate, DescriptionPurchase, Spent
                         FROM Invoicing
                         WHERE Id = @Id AND CAST(PurchaseDate AS DATE) BETWEEN CAST(@StartAt AS DATE) AND CAST(@FinishAt AND DATE)";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Purchases>(query, new{ Id = id, StartAt = startAt, FinishAt = finishAt });
            }
        }

        public async Task<IEnumerable<PaymentHistory>> GetPaymentHistoryAsync(string id)
        {
            var query = $@"SELECT T1.Name, T0.ReferenceMonth, T0.Status, T0.PaymentDate, T0.PaidOut
                        FROM Invoicing T0
                        INNER JOIN Customers T1 ON T1.id = T0.Id
                        WHERE T0.Id = @Id";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                return await connection.QueryAsync<PaymentHistory>(query, new { Id = id });
            }
        }

        public async Task<IEnumerable<Purchases>> GetPurchasesByPeriodAsync(string id, DateTime? startAt, DateTime? finishAt, DateTime? referenceDate)
        {
            var where = new List<string>();

            if (referenceDate.HasValue)
            {
                where.Add($@"AND CAST(PurchaseDate AS DATE) = CAST(@ReferenceDate AS DATE)");
            }
            else
            {
                where.Add($@"AND CAST(PurchaseDate AS DATE) BETWEEN CAST(@StartAt AS DATE) AND CAST(@FinishAt AND DATE)");
            }

            var query = $@"SELECT T1.Name, T0.ReferenceMonth, T0.Status, T0.PaymentDate, T0.PaidOut
                           FROM Invoicing T0
                           INNER JOIN Customers T1 ON T1.id = T0.Id
                           WHERE T0.Id = @Id
                           {string.Join(" ", where)}";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Purchases>(query, new { Id = id, StartAt = startAt, FinishAt = finishAt , RefenceDate = referenceDate });
            }
        }

        public void SendInvoice(string clientId)
        {
            var query = $@"UPDATE Invoicing
                           SET MethodSend = 'ForAdress'
                           WHERE Id = @Id";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                 connection.Execute(query, new { Id = clientId});
            }
        }
    }
}
