using AuktionProjekt.Models.Entities;
using System.Collections.Generic;

namespace AuktionProjekt.Models.Repositories
{
    public interface IBidRepo
    {
        void PlaceBid(Bid bid);

        List<Bid> GetBids(int auctionID);
    }
}

