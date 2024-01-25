using AuktionProjekt.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuktionProjekt.ServiceLayer.IService
{
    public interface IAuctionService
    {
        bool CreateAuction(Auction auction);
        List<Auction> GetAllActiveAuctions();
        List<Auction> SearchAuctions(string search);
        bool DeleteAuction(int auctionID, int loggedInUserID);

    }
}
