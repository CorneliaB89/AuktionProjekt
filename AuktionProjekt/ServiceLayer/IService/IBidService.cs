using AuktionProjekt.Models.DTO;
using AuktionProjekt.Models.Entities;

namespace AuktionProjekt.ServiceLayer.IService
{
    public interface IBidService
    {
        Tuple<BidDTO, int> GetWinningBid(int AuctionID);
        List<Bid>? GetBids(int auctionId);
        decimal PlaceBid(Bid bid);
    }
}
