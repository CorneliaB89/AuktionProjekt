using AuktionProjekt.Models.DTO;
using AuktionProjekt.Models.Entities;
using AuktionProjekt.Models.Repositories;
using AuktionProjekt.ServiceLayer.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace AuktionProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;
        
        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
           
        }

        // Logik för att skapa en auktion.
        [Route("Create_Auctions")]
        [HttpPost]
        [Authorize]
        public IActionResult CreateAuction(CreateAuctionDTO auction)
        {
            try
            {
               
                var inloged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int id = int.Parse(inloged);
                bool check = _auctionService.CreateAuction(auction, id);  
                
                if (check)
                {
                    return Ok("Auction skapad");
                }
                return BadRequest("Glöm inte att lägga till all information");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error, Create Auction");
                throw;
            }
        }

        //Logik för att ta fram alla auktioner.
        
        [HttpGet]
        [Route("All_Active_Auctions")]
        public IActionResult GetAllActiveAuctions()
        {
            try
            {
                var activeAuctions = _auctionService.GetAllActiveAuctions();
               
                return Ok(activeAuctions);
            }
            catch (Exception)
            {
                return StatusCode(500,"Error, Getting All Active Auctions.");
                throw;
            }
        }

        //logik för att söka på auktioner.
        
        [HttpGet("{search}/Auctions")]
        public IActionResult SearchAuctions(string search)
        {
            try
            {
              var searchedAuctions = _auctionService.SearchAuctions(search);
                return Ok(searchedAuctions);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error, Searching Auctions.");
                throw;
            }
        }

        
        [Authorize]
        [HttpDelete("{auctionID}")]
        public IActionResult DeleteAuction(int auctionID)
        {
            try
            {
               
                // Hämta specific userId.
                var loggedInUserID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int inloggedUser= int.Parse(loggedInUserID);
                bool deleteauction = _auctionService.DeleteAuction(auctionID, inloggedUser);
                if (deleteauction)
                {
                    return Ok($"Auktionen med id: {auctionID} har tagits bort");
                }
                return BadRequest("Ej behörig att ta bort denna auktion / Finns ingen auction med detta id / Auktionen har bud och kan inte tas bort");
              }
            catch (Exception)
            {
                return StatusCode(500,"Error, Delete Auction.");
                throw;
            }
        }
    }
}
