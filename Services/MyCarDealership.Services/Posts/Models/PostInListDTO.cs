namespace MyCarDealership.Services.Posts.Models
{
    using Cars.Models;

    public class PostInListDTO
    {
        public CarInListDTO Car { get; init; }

        public string PublishedOn { get; init; }
    }
}