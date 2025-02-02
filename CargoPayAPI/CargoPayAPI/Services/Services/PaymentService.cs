using CargoPayAPI.DAL.Context;
using CargoPayAPI.DAL.Entities;
using CargoPayAPI.External_Services.UFE;
using CargoPayAPI.Models;
using CargoPayAPI.Repos.CardRepo;
using CargoPayAPI.Repos.PaymentRepo;
using CargoPayAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CargoPayAPI.Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepo _paymentRepo;
        private readonly ICardRepo _cardRepo;
        private readonly IUFE _ufe;
        private readonly DatabaseContext _context;

        public PaymentService(IPaymentRepo paymentRepo, ICardRepo cardRepo, IUFE ufe, DatabaseContext context)
        {
            _paymentRepo = paymentRepo;
            _cardRepo = cardRepo;
            _ufe = ufe;
            _context = context;
        }

        public async Task<Payment> CreatePayAsync(Payment payment)
        {
            try 
            {
                return await _paymentRepo.CreatePayAsync(payment);
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.InnerException?.Message ?? exception.Message);
            }
        }

        public async Task<Payment> GetPaymentByIdAsync(Guid id)
        {
            return await _paymentRepo.GetPaymentByIdAsync(id);
        }

        public async Task<Payment> UpdatePaymentStatusAsync(Guid id, PaymentStatus status)
        {
            var payment = await _paymentRepo.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                throw new KeyNotFoundException("Payment not found");
            }

            payment.Status = status;
            return await _paymentRepo.UpdatePayAsync(payment);
        }

        public async Task<Payment> PayAsync(Payment payment)
        {
            //Get Card
            var card = await _cardRepo.GetCardByIdAsync(payment.CardId);
            if (card == null) { throw new Exception("Card Id not found"); }

            //Verify Balance
            if (card.Balance < payment.Amount) { throw new Exception("Insufficient Balance"); }

            //Get fee value
            decimal currentFee = _ufe.GetCurrentFee();
            decimal finalAmount = payment.Amount * currentFee;

            //Updtae card balance
            card.Balance -= finalAmount;
            await _cardRepo.UpdateCardAsync(card);

            //Create pay register
            payment.Amount = finalAmount;
            payment.Status = PaymentStatus.Pending;
            await _paymentRepo.CreatePayAsync(payment);

            return payment;
        }
    }
}
