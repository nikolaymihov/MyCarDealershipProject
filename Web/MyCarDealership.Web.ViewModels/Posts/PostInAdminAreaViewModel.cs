namespace MyCarDealership.Web.ViewModels.Posts
{
    using Cars;

    public class PostInAdminAreaViewModel
    {
        public BaseCarViewModel Car { get; init; }

        public string PublishedOn { get; init; }

        public bool IsPublic { get; init; }
    }
}
