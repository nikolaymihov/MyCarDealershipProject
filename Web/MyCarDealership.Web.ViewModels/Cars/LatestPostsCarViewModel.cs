namespace MyCarDealership.Web.ViewModels.Cars
{
    public class LatestPostsCarViewModel : BaseCarViewModel
    {
        public int Horsepower { get; set; }

        public string FuelType { get; init; }

        public string TransmissionType { get; init; }

        public string CoverImage { get; init; }
    }
}