using AuktionProjekt.Models.Entities;
using AuktionProjekt.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuktionProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionRepo _auctionRepo;
        private readonly IBidRepo _bidRepo;
        public AuctionController(IAuctionRepo auctionRepo, IBidRepo bidRepo)
        {
            _auctionRepo = auctionRepo;
            _bidRepo = bidRepo;
        }

        // Logik för att skapa en auktion.
        [HttpPost]
        [Authorize]
        public IActionResult CreateAuction(Auction auction)
        {
            if (auction.Title == null || auction.Description == null || auction.Price.ToString() == null)
                return BadRequest("Glöm inte att lägga till all information");

            var inloged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            auction.User.UserID = int.Parse(inloged);
            _auctionRepo.CreateAuction(auction);
            return Ok();
        }

        //Logik för att ta fram alla auktioner.
        [HttpGet]
        public IActionResult GetAllAuctions()
        {
            var allAuctions = _auctionRepo.GetAllAuctions();
            var activeAuctions = new List<Auction>();
            foreach (var action in allAuctions)
            {
                if (action.EndDate < DateTime.Now)
                {
                    activeAuctions.Add(action);
                }
            }
            return Ok(activeAuctions);
        }

        //logik för att söka på auktioner.
        [HttpGet]
        public IActionResult SearchAuctions(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                // Om sökparametern är tom returneras ett felmeddelande.
                return BadRequest("Auktionens namn krävs.");
            }

            var searchedAuctions = _auctionRepo.SearchAuctions(search);

            if (searchedAuctions == null || searchedAuctions.Count == 0)
            {
                // Om inga auktioner hittas returneras ett svar.
                return NotFound("Inga auktioner hittades för den angivna sökande.");
            }
            return Ok(searchedAuctions);
        }
        [Authorize]
        [HttpDelete("{auctionID}")]
        public IActionResult DeleteAuction(int auctionID)
        {
            // Hämta bud för auktionen
            var bids = _bidRepo.GetBid(auctionID);

            // Hämta specific userId.
            var loggedInUserID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //Kolla behörighet för vald acution.
            var auction = _auctionRepo.GetAuctionById(auctionID);

            if (auction is null)
                return BadRequest("Finns ingen auction med detta id");

            if (auction.User.UserID != int.Parse(loggedInUserID))
            {
                return Unauthorized("Ej behörig att ta bort denna auktion");
            }
            // Kontrollera att de ej finns några bud på auktionen
            if (!bids.Any())
            {
                // Ta bort auktionen om inga bud finns
                _auctionRepo.DeleteAuction(auctionID);
                return Ok("Auktionen har tagits bort");
            }
            else
            {
                return BadRequest("Auktionen har bud och kan inte tas bort");
            }
        }
    }
}
