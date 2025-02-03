using CargoPayAPI.DAL.Entities;
using CargoPayAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CargoPayAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost, ActionName("Pay")]
        [Route("Pay")]
        public async Task<IActionResult> Pay(Payment payment)
        {
            try
            {
                var createdPayment = await _paymentService.PayAsync(payment);
                return Ok($"The transaction has been delivered succesfully.\nID: {payment.Id}.\nAmount Transfered {payment.Amount}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult> GetPaymentById(Guid id)
        {
            if (id == null) return BadRequest("Id neccesary");

            var payment = await _paymentService.GetPaymentByIdAsync(id);

            if (payment == null) return NotFound($"ID {id} not found");

            return Ok(payment);
        }
    }
}
