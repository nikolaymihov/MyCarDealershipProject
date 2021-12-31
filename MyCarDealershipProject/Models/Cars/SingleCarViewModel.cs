namespace MyCarDealershipProject.Models.Cars
{
    using System.Collections.Generic;

    public class SingleCarViewModel
    {
        public int Id { get; init; }

        public string Make { get; init; }

        public string Model { get; init; }

        public string Description { get; init; }

        public int Year { get; init; }

        public decimal Price { get; init; }

        public int Kilometers { get; init; }

        public string Category { get; init; }

        public string FuelType { get; init; }

        public string TransmissionType { get; init; }

        public ICollection<string> Images { get; init; }

        public ICollection<string> Extras { get; init; }

        public string LocationCountry { get; init; }

        public string LocationCity { get; init; }
    }
}