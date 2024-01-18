using AuktionProjekt.Models.Entities;
using AuktionProjekt.Models.Repositories;
using AuktionProjekt.Repository.Repo;
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
        private readonly IBidRepo _bidRepo;
        private readonly IAuctionRepo _askRepo;
        private readonly IMapper _mapper;


        public BidController(IBidRepo bidRepo, IAuctionRepo askRepo, IMapper mapper)
        {
            _bidRepo = bidRepo;
            _askRepo = askRepo;
            _mapper = mapper;
        }
        [HttpPost]
        [Authorize]
        public IActionResult PlaceBid(Bid bid)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bid.User.UserID = int.Parse(userId);

            var auction = _askRepo.GetAuctionById(bid.Auction.AuctionID);

            if (auction is null)
                return NotFound("Den här auctionen finns inte");

            var bids = _bidRepo.GetBids(bid.Auction.AuctionID);

            if (auction.EndDate < DateTime.Now)
                return BadRequest("Den här auktion är stängd");

            if (auction.User.UserID == bid.User.UserID)
                return BadRequest("Du kan inte lägga bud på din egen auktion");

            foreach (var b in bids)
            {
                if (b.Price <= bid.Price)
                    return BadRequest("Du måste lägga ett högre bud än vad som redan är lagt.");
            }

            _bidRepo.PlaceBid(bid);
            return Ok($"Bud på {bid.Price} kr är lagt.");
        }



        [HttpGet("{auctionId}")]
        [Authorize]
        public IActionResult GetBids(int auctionId)
        {
            var bids = _bidRepo.GetBids(auctionId);

            if (bids == null)
            {
                return NotFound();
            }

            return Ok(bids);
        }

        [Route("WinningBid")]
        [HttpGet("{auctionID}")]
        public IActionResult GetWinningBid(int AuctionID)
        {
            var auction = _askRepo.GetAuctionById(AuctionID);
            if (auction is null)
                return NotFound("Den här auctionen finns inte");

            if (auction.EndDate > DateTime.Now)
                return BadRequest("Den här auktion är fortfarande öppen");

            var highestbid = new Bid() { Price = 0 };

            var bids = _bidRepo.GetBids(AuctionID);
            foreach (var b in bids)
            {
                if (b.Price > highestbid.Price)
                    highestbid = b;
            }
            var Winningbid = _mapper.Map<Bid>(highestbid);

            return Ok(Winningbid);
        }
    }


}


