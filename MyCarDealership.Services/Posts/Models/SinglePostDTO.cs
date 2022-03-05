namespace MyCarDealership.Services.Posts.Models
{
    using Cars.Models;

    public class SinglePostDTO
    {
        public SingleCarDTO Car { get; init; }

        public string PublishedOn { get; init; }

        public string SellerName { get; set; }

        public string SellerPhoneNumber { get; set; }
    }
}