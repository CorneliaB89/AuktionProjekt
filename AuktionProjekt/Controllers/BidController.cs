using AuktionProjekt.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuktionProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {

        [HttpPost]
        [Authorize]
        public IActionResult PlaceBid(Bid bid)
        {
            // Logic to place a bid for an auction
            
            return Ok();
        }

        [HttpGet("{bidId}")]
        [Authorize]
        public IActionResult GetBidDetails(int bidId) //Kommer nog inte behöva det
        {
            // Logic to get details of a specific bid
  
            return Ok();
        }

        [HttpGet("auction/{auctionId}")]
        [Authorize]
        public IActionResult GetBidsForAuction(int auctionId)
        {
            // Logic to get all bids for a specific auction

            return Ok();
        }
        [HttpGet("{auctionID}")]
        public IActionResult GetWinningBid(int auctionID)
        {
            // Logic to cancel a bid

            return Ok();
        }


        //[HttpDelete("{bidId}")]
        //[Authorize]
        //public IActionResult CancelBid(int bidId)
        //{
        //    return Ok();
        //}
        // Vi kommer inte behöva det
    }
}

