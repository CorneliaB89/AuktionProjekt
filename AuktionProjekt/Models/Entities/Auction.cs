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

        public Auction(int auctionID, string title, string description, decimal price, User user)
        {
            AuctionID = auctionID;
            Title = title;
            Description = description;
            Price = price;
            User = user;
        }
        public Auction(User user)
        {
            User =user;
        }
        public Auction()
        {
            
        }

    }
}
