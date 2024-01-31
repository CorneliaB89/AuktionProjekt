using AuktionProjekt.Models.DTO;
using AuktionProjekt.Models.Entities;
using AutoMapper;

namespace AuktionProjekt.Models.Profiles
{
    public class BidProfile :Profile
    {
        public BidProfile()
        {
            CreateMap<Bid, BidDTO>().ForMember(dest => dest.Bidder,
                opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Auction.Title))
                .ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Auction.Description))
                .ForMember(dest => dest.Bid,
                opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.BidTime,
                opt => opt.MapFrom(src => src.BidTime))
                .ForMember(dest => dest.AuctionEndTime,
                opt => opt.MapFrom(src => src.Auction.EndDate));
        }
        
    }
}
