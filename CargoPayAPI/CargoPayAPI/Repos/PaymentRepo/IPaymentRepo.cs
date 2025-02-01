using CargoPayAPI.DAL.Entities;

namespace CargoPayAPI.Repos.PaymentRepo
{
    public interface IPaymentRepo
    {
        /*GetAll or Delete methods has not been implementated because for payment this is not necessary, also Update method will be structurated
         with the idea that it can only edit a Status from payment*/
        Task<Payment> CreatePayAsync(Payment payment);
        Task<Payment> GetPaymentByIdAsync(Guid id);
        Task<Payment> UpdatePayAsync(Payment payment);
    }
}
