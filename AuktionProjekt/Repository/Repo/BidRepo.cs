using AuktionProjekt.Models.DTO;
using AuktionProjekt.Models.Entities;
using AuktionProjekt.Models.Repositories;
using AuktionProjekt.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AuktionProjekt.Repository.Repo
{
    public class BidRepo:IBidRepo
    {
        private readonly IAucktionDBContext _dbContext;
        public BidRepo(IAucktionDBContext aucktionDBContext)
        {
            _dbContext = aucktionDBContext;
        }
        public List<Bid>? GetBids(int auctionID) 
        {
            

            using (IDbConnection db =  _dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AuctionID", auctionID);

                var searchedBids = db.Query<Bid, User,Auction, Bid>("GetBid",
                      (bids,user,auctions) =>
                      {
                          bids.User = user;
                          bids.Auction= auctions;
                          return bids;

                      },param:parameters, splitOn: "Username, AuctionID", commandType: CommandType.StoredProcedure).ToList();
                return searchedBids;
            }
        }
        public void PlaceBid (PlaceBidDTO bid, int id)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AuctionID", bid.AuctionsId);
                parameters.Add("@UserID", id);
                parameters.Add("@Price", bid.Price);

                db.Execute("PlaceBid", parameters, commandType:CommandType.StoredProcedure);

            }

        }
    }
}
