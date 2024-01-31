using AuktionProjekt.Models.DTO;
using AuktionProjekt.Models.Entities;

namespace AuktionProjekt.ServiceLayer.IService
{
    public interface IBidService
    {
        (BidDTO, int) GetWinningBid(int AuctionID);
        List<BidDTO>? GetBids(int auctionId);
        decimal PlaceBid(PlaceBidDTO bid, int id);
    }
}
