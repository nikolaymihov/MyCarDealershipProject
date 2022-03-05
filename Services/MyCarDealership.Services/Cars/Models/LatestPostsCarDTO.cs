namespace MyCarDealership.Services.Cars.Models
{
    public class LatestPostsCarDTO : BaseCarDTO
    {
        public int Horsepower { get; set; }

        public string FuelType { get; init; }

        public string TransmissionType { get; init; }

        public string CoverImage { get; init; }
    }
}