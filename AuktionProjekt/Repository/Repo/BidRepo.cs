using AuktionProjekt.Models.Entities;
using System.Data;

namespace AuktionProjekt.Repository.Repo
{
    public class BidRepo
    {
        public List<Auction> GetBid() //Osäker om jag har tänkt rätt
        {
            using (IDbConnection db = /*Databasconnectionstring*/)
            {
                
                var searchedBids = db.Query<Auction, Bid, Auction>("GetBid",
                      (auctions, bids) =>
                      {
                          auctions.Bids = bids;
                          return bids;

                      }, splitOn: "AuctionID", commandType: CommandType.StoredProcedure).ToList();
                return searchedBids;
            }
        }
    }
}
