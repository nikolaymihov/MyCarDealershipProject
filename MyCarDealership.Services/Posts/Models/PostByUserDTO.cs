namespace MyCarDealership.Services.Posts.Models
{
    using Cars.Models;

    public class PostByUserDTO
    {
        public CarByUserDTO Car { get; init; }

        public string PublishedOn { get; init; }
    }
}