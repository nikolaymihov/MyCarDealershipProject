namespace MyCarDealership.Services.Cars.Models
{
    using System.Collections.Generic;

    public class SingleCarDTO : BaseCarDTO
    {
        public string Description { get; init; }

        public int Kilometers { get; init; }

        public int Horsepower { get; set; }

        public string Category { get; init; }

        public string FuelType { get; init; }

        public string TransmissionType { get; init; }

        public IList<string> Images { get; init; }

        public ICollection<string> ComfortExtras { get; init; }

        public ICollection<string> SafetyExtras { get; init; }

        public ICollection<string> OtherExtras { get; init; }

        public string LocationCountry { get; init; }

        public string LocationCity { get; init; }
    }
}