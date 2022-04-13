namespace MyCarDealership.Services.Posts.Models
{
    using Cars.Models;

    public class BasePostInListDTO
    {
        public BaseCarDTO Car { get; init; }

        public string PublishedOn { get; init; }

        public bool IsPublic { get; init; }
    }
}