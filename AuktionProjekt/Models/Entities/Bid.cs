namespace AuktionProjekt.Models.Entities
{
    public class Bid
    {
        public int BidID { get; set; }  
        public Auction Auction { get; set;}
        public User User { get; set; }
        public decimal Price  { get; set; }
        public DateTime BitTime { get; set; }
    }
}
