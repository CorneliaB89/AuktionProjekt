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
            return Ok();
        }

        [HttpGet("{bidId}")]
        [Authorize]
        public IActionResult GetBidDetails(int bidId) //Kommer nog inte behöva det
        {

            return Ok();
        }

        [HttpGet("auction/{auctionId}")]
        [Authorize]
        public IActionResult GetBidsForAuction(int auctionId)
        {
 
            return Ok();
        }
        [HttpGet("{auctionID}")]
        public IActionResult GetWinningBid(int auctionID)
        {
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

