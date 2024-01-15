namespace AuktionProjekt.Models.Entities
{
    public class Auction
    {
        public int AuctionID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User User { get; set; }

    }
}
