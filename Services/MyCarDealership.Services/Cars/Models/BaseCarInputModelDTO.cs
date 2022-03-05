namespace MyCarDealership.Services.Cars.Models
{
    using System.Collections.Generic;

    public class BaseCarInputModelDTO
    {
        public int CategoryId { get; init; }

        public IEnumerable<BaseCarSpecificationServiceModel> Categories { get; set; }
        
        public int FuelTypeId { get; init; }

        public IEnumerable<BaseCarSpecificationServiceModel> FuelTypes { get; set; }
        
        public int TransmissionTypeId { get; init; }

        public IEnumerable<BaseCarSpecificationServiceModel> TransmissionTypes { get; set; }
        
        public int CarExtraId { get; init; }

        public IEnumerable<CarExtrasServiceModel> CarExtras { get; set; }
    }
}
