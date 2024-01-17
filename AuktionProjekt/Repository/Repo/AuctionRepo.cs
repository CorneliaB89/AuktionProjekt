using AuktionProjekt.Models.Entities;
using AuktionProjekt.Models.Repositories;
using AuktionProjekt.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AuktionProjekt.Repository.Repo
{
    public class AuctionRepo : IAuctionRepo
    {
        private readonly IAucktionDBContext _dbContext;
        public AuctionRepo(IAucktionDBContext aucktionDBContext)
        {
            _dbContext = aucktionDBContext;
        }


        public void CreateAuction(Auction auction)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Title", auction.Title);
                parameters.Add("@Description", auction.Description);
                parameters.Add("@Price", auction.Price);
                parameters.Add("@StartDate", DateTime.Now);
                parameters.Add("@EndDate", DateTime.Now.AddDays(5));
                parameters.Add("@UserID", auction.User.UserID);

                db.Execute("CreateAuction", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteAuction(int auctionId)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AuctionID", auctionId);

                db.Execute("DeleteAuction", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Auction> GetAllAuctions()
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                // Mappar så att UserId hamnar i Auction.User.UserID
                var searchedAuctions = db.Query<Auction, User, Auction>("GetAllAuctions",
                      (auctions, user) =>
                      {
                          auctions.User = user;
                          return auctions;

                      }, splitOn: "UserID", commandType: CommandType.StoredProcedure).ToList();
                return searchedAuctions;
            }
        }

        public Auction? GetAuctionById(int auctionId)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AuctionID", auctionId);

                // Mappar så att UserId hamnar i Auction.User.UserID
                var searchedAuctions = db.Query<Auction, User, Auction>("GetAuctionById",
                    (auctions, user) =>
                    {
                        auctions.User = user;
                        return auctions;

                    }, param: parameters,
                    splitOn: "UserID", commandType: CommandType.StoredProcedure).SingleOrDefault();
                return searchedAuctions;
            }
        }

        public List<Auction>? SearchAuctions(string search)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Search", search);

                // Mappar så att UserId hamnar i Auction.User.UserID
                var searchedAuctions = db.Query<Auction, User, Auction>("SearchAuction",
                    (auctions, user) =>
                    {
                        auctions.User = user;
                        return auctions;

                    }, param: parameters,
                    splitOn: "UserID", commandType: CommandType.StoredProcedure).ToList();
                return searchedAuctions;
            }
        }

        public void UpdateAuction(Auction auction)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AuctionID", auction.AuctionID);
                parameters.Add("@Title", auction.Title);
                parameters.Add("@Description", auction.Description);
                parameters.Add("@Price", auction.Price);
                parameters.Add("@StartDate", DateTime.Now);
                parameters.Add("@EndDate", DateTime.Now.AddDays(5));

                db.Execute("UpdateAuction", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        
    }
}
