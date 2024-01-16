using AuktionProjekt.Models.Entities;
using System.Collections.Generic;

namespace AuktionProjekt.Models.Repositories
{
    public interface IAuctionRepo
    {
        void CreateAuction(Auction auction);
        void DeleteAuction(int auctionId);
        void UpdateAuction(Auction auction);
        List<Auction>? SearchAuctions(string search);
        Auction? GetAuctionById(int auctionId);
        List<Auction> GetAllAuctions();
                
    }
}
