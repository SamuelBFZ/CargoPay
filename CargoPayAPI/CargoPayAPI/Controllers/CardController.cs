using CargoPayAPI.DAL.Entities;
using CargoPayAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CargoPayAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController (ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet, ActionName("GetAll")]
        [Route("GetAll")]
        public async Task<ActionResult<Card>> GetCardAsync()
        {
            var card = await _cardService.GetCardsAsync();

            if (card == null || !card.Any()) return NotFound();

            return Ok(card);
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult<Card>> GetCardByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("Id necessary");

            var card = await _cardService.GetCardByIdAsync(id);

            if (card == null) return NotFound($"ID {id} not found");

            return Ok(card);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateCardAsync(Card card)
        {
            var createdCard = await _cardService.CreateCardAsync(card);
            return Ok(createdCard);
        }
    }
}
