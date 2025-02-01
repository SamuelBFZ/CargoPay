using CargoPayAPI.DAL.Entities;
using CargoPayAPI.Repos.CardRepo;
using CargoPayAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CargoPayAPI.Services.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepo _cardRepo; //Repo inyection

        public CardService(ICardRepo cardRepo)
        {
            _cardRepo = cardRepo;
        }

        public async Task<Card> CreateCardAsync(Card card)
        {
            try
            {
                return await _cardRepo.CreateCardAsync(card);

            }catch (DbUpdateException exception)
            {
                throw new Exception(exception.InnerException?.Message ?? exception.Message);
            }
        }
        public async Task<Card> GetCardByIdAsync(Guid id)
        {
            return await _cardRepo.GetCardByIdAsync(id);
        }

        public async Task<IEnumerable<Card>> GetCardsAsync()
        {
            return await _cardRepo.GetCardsAsync();
        }

        public async Task<Card> UpdateCardAsync(Card card)
        {
            var existingCard = await _cardRepo.GetCardByIdAsync(card.Id);
            if (existingCard == null)
            {
                throw new KeyNotFoundException("Card not found");
            }

            existingCard.Number = card.Number;
            existingCard.Expires = card.Expires;
            existingCard.Cvv = card.Cvv;
            existingCard.Brand = card.Brand;

            return await _cardRepo.UpdateCardAsync(existingCard);
        }

        public async Task<Card> DeleteCardAsync(Guid id)
        {
            return await _cardRepo.DeleteCardAsync(id);
        }

        public async Task<decimal> GetBalanceAsync(Guid id)
        {
            return await _cardRepo.GetBalanceAsync(id);
        }

        public async Task<bool> HasSufficientBalanceAsync(Guid id, decimal amount)
        {
            return await _cardRepo.HasSufficientBalanceAsync(id, amount);
        }
    }
}
