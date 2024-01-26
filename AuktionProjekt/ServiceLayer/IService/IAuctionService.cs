using AuktionProjekt.Models.DTO;
using AuktionProjekt.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuktionProjekt.ServiceLayer.IService
{
    public interface IAuctionService
    {
        bool CreateAuction(CreateAuctionDTO auction, int id);
        List<Auction> GetAllActiveAuctions();
        List<Auction> SearchAuctions(string search);
        bool DeleteAuction(int auctionID, int loggedInUserID);
        int UpdateAuction(Auction auction);

    }
}
