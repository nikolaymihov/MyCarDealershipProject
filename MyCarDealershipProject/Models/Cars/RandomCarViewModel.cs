namespace MyCarDealershipProject.Models.Cars
{
    public class RandomCarViewModel
    {
        public int Id { get; init; }

        public string Make { get; init; }

        public string Model { get; init; }

        public int Year { get; init; }

        public decimal Price { get; init; }

        public int Horsepower { get; set; }

        public string FuelType { get; init; }

        public string TransmissionType { get; init; }

        public string CoverImage { get; init; }
    }
}