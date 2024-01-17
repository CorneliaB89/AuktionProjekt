﻿using AuktionProjekt.Models.Entities;
using AuktionProjekt.Models.Repositories;
using AuktionProjekt.Repository.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
       

        public BidController(IBidRepo bidRepo,IAuctionRepo askRepo)
        {
            _bidRepo = bidRepo;
            _askRepo = askRepo;
        }
        [HttpPost]
        [Authorize]
        public IActionResult PlaceBid(Bid bid)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bid.User.UserID= int.Parse(userId);

            var auction = _askRepo.GetAuctionById(bid.Auction.AuctionID);
           var bids = _bidRepo.GetBid(bid.Auction.AuctionID);
            if (auction.EndDate < DateTime.Now)
                return BadRequest("Den här auktion är stängd");
            if (auction.User.UserID == bid.User.UserID)
                return BadRequest("Du kan inte lägga bud på din egen auktion");

            foreach (var b in bids)
            {
                if (b.Price < bid.Price)
                    return BadRequest("Du kan inte lägga ett lägre bud än det högsta redan lagda.");
               
              
            }
           

            _bidRepo.PlaceBid(bid);
            return Ok("Bud har lagts");
              
           
        }


        
        [HttpGet("{auctionId}")]
        [Authorize]
        public IActionResult GetBid(int auctionId)
        {
            var bids = _bidRepo.GetBid(auctionId);

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
            if (auction.EndDate > DateTime.Now)
            return BadRequest("Den här auktion är fortfarande öppen");

            decimal winingBid = 0;
            var bids = _bidRepo.GetBid(AuctionID);
            foreach (var b in bids)
            {
                if(b.Price >winingBid)
                {
                    winingBid = b.Price;
                }
            }

            return Ok(new { WinningBid = winingBid });
        }
    }

   
}


