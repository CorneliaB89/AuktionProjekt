using AuktionProjekt.Models.Entities;
using AuktionProjekt.Models.Repositories;
using Dapper;
using System.Data;

namespace AuktionProjekt.Repository.Repo
{
    public class BidRepo:IBidRepo
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
        public void PlaceBid (Bid bid)
        {
            using (IDbConnection db =/*Databasconnectionstring*/ )
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AuctionID", bid.Auction.AuctionID);
                parameters.Add("@UserID", bid.User.UserName);
                parameters.Add("@Price", bid.Price);
                parameters.Add("@BidTime",DateTime.Now);

                db.Execute("PlaceBid", parameters, commandType:CommandType.StoredProcedure);

            }

        }
    }
}
