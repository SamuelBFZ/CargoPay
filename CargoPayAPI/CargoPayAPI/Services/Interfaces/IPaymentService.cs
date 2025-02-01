using CargoPayAPI.DAL.Entities;
using CargoPayAPI.Models;

namespace CargoPayAPI.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> CreatePayAsync(Payment payment);
        Task<Payment> GetPaymentByIdAsync(Guid id);
        Task<Payment> UpdatePaymentStatusAsync(Guid id, PaymentStatus status);
        Task<Payment> PayAsync(Payment payment);
    }
}
