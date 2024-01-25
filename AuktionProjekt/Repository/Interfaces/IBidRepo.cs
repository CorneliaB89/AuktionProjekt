using AuktionProjekt.Models.DTO;
using AuktionProjekt.Models.Entities;
using System.Collections.Generic;

namespace AuktionProjekt.Models.Repositories
{
    public interface IBidRepo
    {
        void PlaceBid(PlaceBidDTO bid, int id);

        List<Bid>? GetBids(int auctionID);
    }
}

