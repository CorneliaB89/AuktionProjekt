using AuktionProjekt.Models.Entities;
using System.Collections.Generic;

namespace AuktionProjekt.Models.Repositories
{
    public interface IBidRepo
    {
        void PlaceBid(Bid bid);
        Bid GetBidById(int bidId);
        IEnumerable<Bid> GetBidsForAuction(int auctionId);
        void CancelBid(int bidId);
        //Lägg till flera metoder senare för att synka med databsen.
    }
}

