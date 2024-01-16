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
        public IActionResult GetAuctionDetails(int auctionId) //Kanske inte behöver, (Behövs för att få fram detaljer i auctionen.
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
       
        }
        [HttpGet]
        public IActionResult SearchAuction(string title) 
        {
            return Ok();
        }
    }
}
