namespace MyCarDealershipProject.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Services.Cars.Models;

    public class SearchCarInputModel
    {
        [Display(Name = "Make, model or/and specification:")]
        public string TextSearchTerm { get; set; }
        
        public int? FromYear { get; set; }
        
        public int? ToYear { get; set; }

        public int? MinHorsepower { get; set; }

        public int? MaxHorsepower { get; set; }

        [Display(Name = "Minimum price (in Euro):")]
        public decimal? MinPrice { get; set; }

        [Display(Name = "Maximum price (in Euro):")]
        public decimal? MaxPrice { get; set; }
        
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