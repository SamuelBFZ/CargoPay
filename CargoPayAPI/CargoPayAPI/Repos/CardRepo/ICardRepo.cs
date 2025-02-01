using CargoPayAPI.DAL.Entities;

namespace CargoPayAPI.Repos.CardRepo
{
    public interface ICardRepo
    {
        Task<Card> GetCardByIdAsync(Guid id);
        Task<IEnumerable<Card>> GetCardsAsync();
        Task<Card> CreateCardAsync(Card card);
        Task<Card> UpdateCardAsync(Card card);
        Task<Card> DeleteCardAsync(Guid id);
        Task<decimal> GetBalanceAsync(Guid id);
        Task<bool> HasSufficientBalanceAsync(Guid id, decimal amount); // Verify if card has enough balance

    }
}
