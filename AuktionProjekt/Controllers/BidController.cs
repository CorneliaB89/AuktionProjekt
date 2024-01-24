using AuktionProjekt.Models.Entities;
using AuktionProjekt.Models.Repositories;
using AuktionProjekt.Repository.Repo;
using AuktionProjekt.ServiceLayer.IService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AuktionProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;


        public BidController(IBidService bidService)
        {
            _bidService = bidService;
        }
        [HttpPost]
        [Authorize]
        public IActionResult PlaceBid(Bid bid)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                bid.User.UserID = int.Parse(userId);

                decimal outcome = _bidService.PlaceBid(bid);

                if (outcome == -1)
                    return NotFound("Den här auctionen finns inte");
                if (outcome == -2)
                    return BadRequest("Den här auktion är stängd");
                if (outcome == -3)
                    return BadRequest("Du kan inte lägga bud på din egen auktion");
                if (outcome == -4)
                    return BadRequest("Du måste lägga ett högre bud än vad som redan är lagt.");
                if (outcome == bid.Price)
                    return Ok($"Bud på {bid.Price} kr har lagts.");

                return StatusCode(500, "Error, Placingbid");

            }
            catch (Exception)
            {

                throw;
            }
        }



        [HttpGet("{auctionId}")]
        [Authorize]
        public IActionResult GetBids(int auctionId)
        {
            try
            {
                var bids = _bidService.GetBids(auctionId);
                if (bids is null) return NotFound("Det finns inga bud på denna auktion");

                return Ok(bids);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error, Get Bids");
                throw;
            }
        }

        [Route("WinningBid")]
        [HttpGet("{auctionID}")]
        public IActionResult GetWinningBid(int AuctionID)
        {
            try
            {
                var tuple = _bidService.GetWinningBid(AuctionID);
                var winningbid = tuple.Item1;
                int number = tuple.Item2;

                if (number == 0)
                    return NotFound("Denna auctionfinns inte.");
                if (number == 1)
                    return BadRequest("Den här auktion är fortfarande öppen");
                if (number == -1)
                    return NotFound("Det finns inga bud på denna aktion.");
                return Ok(winningbid);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error, WiningBid");
                throw;
            }
        }
    }


}


