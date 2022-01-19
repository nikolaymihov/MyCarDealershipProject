namespace MyCarDealershipProject.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Services.Cars.Models;

    public class BaseCarInputModel
    {
        [Display(Name = "Category:")]
        public int CategoryId { get; init; }

        public IEnumerable<CarCategoryServiceModel> Categories { get; set; }

        [Display(Name = "Fuel type:")]
        public int FuelTypeId { get; init; }

        public IEnumerable<CarFuelTypeServiceModel> FuelTypes { get; set; }

        [Display(Name = "Transmission type:")]
        public int TransmissionTypeId { get; init; }

        public IEnumerable<CarTransmissionTypeServiceModel> TransmissionTypes { get; set; }

        [Display(Name = "Extras:")]
        public int CarExtraId { get; init; }

        public IEnumerable<CarExtrasServiceModel> CarExtras { get; set; }
    }
}
