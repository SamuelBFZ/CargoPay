using CargoPayAPI.DAL.Entities;

namespace CargoPayAPI.Services.Interfaces
{
    public interface ICardService
    {
        Task<Card> CreateCardAsync(Card card);
        Task<Card> GetCardByIdAsync(Guid id);
        Task<IEnumerable<Card>> GetCardsAsync();
        Task<Card> UpdateCardAsync(Card card);
        Task<Card> DeleteCardAsync(Guid id);
        Task<decimal> GetBalanceAsync(Guid id);
        Task<bool> HasSufficientBalanceAsync(Guid id, decimal amount);
    }
}
