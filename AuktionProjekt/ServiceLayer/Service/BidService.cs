using AuktionProjekt.Models.DTO;
using AuktionProjekt.Models.Entities;
using AuktionProjekt.Models.Repositories;
using AuktionProjekt.ServiceLayer.IService;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace AuktionProjekt.ServiceLayer.Service
{
    public class BidService : IBidService
    {
        private readonly IBidRepo _bidRepo;
        private readonly IAuctionRepo _auctionRepo;
        private readonly IMapper _mapper;
        public BidService(IBidRepo bidRepo, IAuctionRepo auctionRepo, IMapper mapper)
        {
            _bidRepo = bidRepo;
            _auctionRepo = auctionRepo;
            _mapper = mapper;
        }
        public List<Bid>? GetBids(int auctionId)
        {
            return _bidRepo.GetBids(auctionId);
                      
        }

        public Tuple<BidDTO, int> GetWinningBid(int AuctionID)
        {
            var bidDTO = new BidDTO();
            var auction = _auctionRepo.GetAuctionById(AuctionID);
            if (auction is null)
                return Tuple.Create(bidDTO, 0); //NotFound Denna auctionfinns inte.

            if (auction.EndDate > DateTime.Now)
                return Tuple.Create(bidDTO, 1); //BadRequest("Den här auktion är fortfarande öppen");

            var highestbid = new Bid() { Price = 0 };

            var bids = _bidRepo.GetBids(AuctionID);

            if (bids is null)
                return Tuple.Create(bidDTO, -1);

            foreach (var b in bids)
            {
                if (b.Price > highestbid.Price)
                    highestbid = b;
            }
            var winningBid = _mapper.Map<BidDTO>(highestbid);

            return Tuple.Create(winningBid,2); // OK
        }

        public decimal PlaceBid(Bid bid)
        {
            try
            {
                var auction = _auctionRepo.GetAuctionById(bid.Auction.AuctionID);
                if (auction is null)
                    return -1; //Notfound Den här auctionen finns inte

                var bids = _bidRepo.GetBids(bid.Auction.AuctionID);

                if (auction.EndDate < DateTime.Now)
                    return -2; //Bad request Den här auktion är stängd

                if (auction.User.UserID == bid.User.UserID)
                    return -3; //badrequest   Du kan inte lägga bud på din egen auktion

                foreach (var b in bids)
                {
                    if (b.Price <= bid.Price)
                        return -4; //return BadRequest("Du måste lägga ett högre bud än vad som redan är lagt.");
                }
                _bidRepo.PlaceBid(bid);
                return bid.Price; //OK bud har lagts
            }
            catch (Exception)
            {
                return 0; //return statuscode 500, Error Placebid
                throw;
            }


            
        }
    }
}
