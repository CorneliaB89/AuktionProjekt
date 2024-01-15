using AuktionProjekt.Models.Entities;
using System.Collections.Generic;

namespace AuktionProjekt.Models.Repositories
{
    public interface IAuctionRepo
    {
        void CreateAuction(Auction auction);
        Auction GetAuctionById(int auctionId);
        IEnumerable<Auction> GetAllAuctions();
        void CloseAuction(int auctionId);
        // Lägg till flera metoder för att synka med databsen.
    }
}
