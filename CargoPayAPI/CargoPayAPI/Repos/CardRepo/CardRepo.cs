using CargoPayAPI.DAL.Context;
using CargoPayAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoPayAPI.Repos.CardRepo
{
    public class CardRepo : ICardRepo
    {
        private readonly DatabaseContext _context;

        public CardRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Card> GetCardByIdAsync(Guid id)
        {
            return await _context.Cards.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Card>> GetCardsAsync()
        {
            return await _context.Cards.ToListAsync();
        }

        public async Task<Card> CreateCardAsync(Card card)
        {
                card.Id = Guid.NewGuid();

                _context.Cards.Add(card);
                await _context.SaveChangesAsync();

                return card;
        }

        public async Task<Card> UpdateCardAsync(Card card)
        {
                _context.Cards.Update(card);
                await _context.SaveChangesAsync();

                return card;
        }

        public async Task<Card> DeleteCardAsync(Guid id)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(b => b.Id == id);
            if (card == null) return null;

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return card;
        }

        public async Task<decimal> GetBalanceAsync(Guid id)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(b => b.Id == id);
            if (card == null) throw new KeyNotFoundException($"ID {id} Not found");
            return card.Balance;
        }

        public async Task<bool> HasSufficientBalanceAsync(Guid id, decimal amount)
        {
            return await _context.Cards.AnyAsync(c => c.Id == id && c.Balance >= amount);
        }
    }
}
