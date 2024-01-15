using AuktionProjekt.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuktionProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {

        [HttpPost]
        [Authorize]
        public IActionResult CreateAuction(Auction auction)
        {
            // Logic to create a new auction
            return Ok();
        }

        [HttpGet("{auctionId}")]
        [Authorize]
        public IActionResult GetAuctionDetails(int auctionId) //Kanske inte behöver .............
        {
            // Logic to get details of a specific auction
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllAuctions()
        {
            // Logic to get a list of all auctions
            return Ok();
        }

        [HttpGet("{auctionId}/bids")]
        [Authorize]
        public IActionResult GetBidsForAuction(int auctionId) //Har flyttat denna i bidControllern (kan ta bort) ..........
        {
            // Logic to get all bids for a specific auction
            return Ok();
        }

        [HttpGet("{auctionId}/user")]
        [Authorize]
        public IActionResult GetAuctionOwner(int auctionId)  //Behövs inte (kan ta bort) ...........
        {
            // Logic to get the user (owner) of a specific auction
            return Ok();
        }

        [HttpDelete("{auctionId}")]
        [Authorize]
        public IActionResult CloseAuction(int auctionId)  // kommer ej behövas (kan ta bort) ..........
        {
            // Logic to close an auction
            return Ok();
        }
        [HttpGet]
        public IActionResult SearchAuction(string title) 
        {
            return Ok();
        }
    }
}
