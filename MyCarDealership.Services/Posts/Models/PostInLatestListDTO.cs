namespace MyCarDealership.Services.Posts.Models
{
    using Cars.Models;

    public class PostInLatestListDTO
    {
        public LatestPostsCarDTO Car { get; init; }

        public string PublishedOn { get; init; }
    }
}