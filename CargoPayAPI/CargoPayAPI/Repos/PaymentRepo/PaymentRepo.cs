using CargoPayAPI.DAL.Context;
using CargoPayAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoPayAPI.Repos.PaymentRepo
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly DatabaseContext _context;

        public PaymentRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Payment> CreatePayAsync(Payment payment)
        {
            payment.Id = Guid.NewGuid();

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return payment;
        }

        public async Task<Payment> GetPaymentByIdAsync(Guid id)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Payment> UpdatePayAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();

            return payment;
        }
    }
}
