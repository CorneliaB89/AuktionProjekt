using AuktionProjekt.Models.Entities;
using Dapper;
using System.Data;

namespace AuktionProjekt.Repository.Repo
{
    public class BidRepo
    {
        public List<Bid> GetBid(int auctionID) 
        {
            using (IDbConnection db = /*Databasconnectionstring*/)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AuctionID", auctionID);

                var searchedBids = db.Query<Bid, User,Auction, Bid>("GetBid",
                      (bids,user,auctions) =>
                      {
                          bids.User = user;
                          bids.Auction= auctions;
                          return bids;

                      },param:parameters, splitOn: "Username,AuctionID", commandType: CommandType.StoredProcedure).ToList();
                return searchedBids;
            }
        }
    }
}
