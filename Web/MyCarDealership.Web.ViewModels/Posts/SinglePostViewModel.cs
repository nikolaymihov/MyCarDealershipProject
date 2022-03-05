namespace MyCarDealership.Web.ViewModels.Posts
{
    using Cars;

    public class SinglePostViewModel
    {
        public SingleCarViewModel Car { get; init; }

        public string PublishedOn { get; init; }

        public string SellerName { get; set; }

        public string SellerPhoneNumber { get; set; }
    }
}