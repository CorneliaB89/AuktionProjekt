using AuktionProjekt.Models.Entities;
using AuktionProjekt.Models.Repositories;
using AuktionProjekt.ServiceLayer.IService;
using System.Security.Claims;

namespace AuktionProjekt.ServiceLayer.Service
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepo _auctionRepo;
        private readonly IBidRepo _bidRepo;
        public AuctionService(IAuctionRepo auctionRepo, IBidRepo bidRepo)
        {
            _auctionRepo = auctionRepo;
            _bidRepo = bidRepo;
        }
        public bool CreateAuction(Auction auction)
        {
            if (auction.Title == null || auction.Description == null || auction.Price.ToString() == null)
                return false; //BadRequest("Glöm inte att lägga till all information");

            
            _auctionRepo.CreateAuction(auction);

            return true; //("Auction skapad");
        }

        public bool DeleteAuction(int auctionID, int loggedInUserID)
        {
            // Hämta bud för auktionen
            var bids = _bidRepo.GetBids(auctionID);

           

            //Kolla behörighet för vald acution.
            var auction = _auctionRepo.GetAuctionById(auctionID);

            if (auction is null)
                return false; //BadRequest("Finns ingen auction med detta id");

            if (auction.User.UserID !=loggedInUserID)
            {
                return false; //Unauthorized("Ej behörig att ta bort denna auktion");
            }
            // Kontrollera att de ej finns några bud på auktionen
            if (!bids.Any())
            {
                // Ta bort auktionen om inga bud finns
                _auctionRepo.DeleteAuction(auctionID);
                return true; //Ok("Auktionen har tagits bort");
            }
            else
            {
                return false;  //BadRequest("Auktionen har bud och kan inte tas bort");
            }
        }

        public List<Auction> GetAllActiveAuctions()
        {
            var activeAuctions = _auctionRepo.GetAllAuctions().Where(a => a.EndDate < DateTime.Now);

            return activeAuctions.ToList(); 
        }

        public List<Auction> SearchAuctions(string search)
        {
           

            var searchedAuctions = _auctionRepo.SearchAuctions(search);

          
            return searchedAuctions;
        }
    }
}
