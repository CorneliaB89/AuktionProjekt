namespace AuktionProjekt.Models.DTO
{
    public class BidDTO
    {
        public string Bidder { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal WinningBid { get; set; }
        public DateTime WinningBidTime { get; set;}
        public DateTime AuctionEndTime { get; set; }
    }
}
