using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteCredit.Domains.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TesteCredit.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ManagementClientController : Controller
    {
        private readonly ICreditCardRepository _creditCardRepository;

        public ManagementClientController(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

        [HttpGet("purchases/{id}/{refenceDate}")]
        public async Task<IActionResult> GetPurchaseByReferenceAsync(string id, DateTime referenceDate)
        {
            return Ok(await _creditCardRepository.GetPurchasesByPeriodAsync(id, null, null, referenceDate));
        }

        [HttpGet("purchases/{id}/{startAt}/{finishAt}")]
        public async Task<IActionResult> GetPurchaseByPeriodAsync(string id, DateTime startAt, DateTime finishAt)
        {
            return Ok(await _creditCardRepository.GetPurchasesByPeriodAsync(id, startAt, finishAt, null));
        }

        [HttpGet("invoicing/{id}")]
        public async Task<IActionResult> GetInvoiceByPeriodAsync(string id)
        {
            return Ok(await _creditCardRepository.GetPaymentHistoryAsync(id));
        }

        [HttpPatch("alter-method-send/{id}")]
        public IActionResult PatchAlterSend(string id)
        {
            _creditCardRepository.SendInvoice(id);
            return Ok(new { message = "Tipo de envio alterado. " });
        }
    }
}
