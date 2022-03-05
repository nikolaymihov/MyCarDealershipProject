namespace MyCarDealership.Services.Cars.Models
{
    public class BaseCarDTO
    {
        public int Id { get; init; }

        public string Make { get; init; }

        public string Model { get; init; }

        public int Year { get; init; }

        public decimal Price { get; init; }
    }
}