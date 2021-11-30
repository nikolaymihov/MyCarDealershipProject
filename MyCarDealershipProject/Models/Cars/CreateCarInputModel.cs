namespace MyCarDealershipProject.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using Services.Cars.Models;

    public class CreateCarInputModel : BaseCarInputModel
    {
        [Display(Name = "Car category:")]
        public int CategoryId { get; init; }

        public IEnumerable<CarCategoryServiceModel> Categories { get; set; }

        [Display(Name = "Car fuel type:")]
        public int FuelTypeId { get; init; }

        public IEnumerable<CarFuelTypeServiceModel> FuelTypes { get; set; }

        [Display(Name = "Car transmission type:")]
        public int TransmissionTypeId { get; init; }

        public IEnumerable<CarTransmissionTypeServiceModel> TransmissionTypes { get; set; }

        public int CarExtraId { get; init; }
        
        public IEnumerable<CarExtrasServiceModel> CarExtras { get; set; }
        

        [Display(Name = "Car images:")]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
