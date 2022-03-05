namespace MyCarDealership.Web.ViewModels.Posts
{
    using Cars;

    public class PostByUserViewModel
    {
        public CarByUserViewModel Car { get; init; }

        public string PublishedOn { get; init; }
    }
}