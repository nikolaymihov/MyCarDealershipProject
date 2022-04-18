namespace MyCarDealership.Web.ViewModels.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using CustomAttributes.ValidationAttributes;

    using static Data.Common.DataConstants;

    public class CarFormInputModel : BaseCarInputModel
    {
        [Required(ErrorMessage = "The car make field is required.")]
        [StringLength(
            CarMakeMaxLength,
            MinimumLength = CarMakeMinLength,
            ErrorMessage = "The car make must be between {2} and {1} characters long.")]
        [Display(Name = "Make:")]
        public string Make { get; set; }

        [Required(ErrorMessage = "The car model field is required.")]
        [StringLength(
            CarModelMaxLength,
            MinimumLength = CarModelMinLength,
            ErrorMessage = "The car model must be between {2} and {1} characters long.")]
        [Display(Name = "Model:")]
        public string Model { get; set; }

        [Required(ErrorMessage = "The car description field is required.")]
        [StringLength(
            int.MaxValue,
            MinimumLength = CarDescriptionMinLength,
            ErrorMessage = "The car description must be at least {2} characters long.")]
        [Display(Name = "Description:")]
        public string Description { get; set; }

        [RangeUntilCurrentYear(
            CarYearMinValue,
            ErrorMessage = "The car year must be between {1} and {2}.")]
        [Display(Name = "Year of first registration:")]
        public int Year { get; set; }

        [RangeWithCustomFormat(CarPriceMinValue, CarPriceMaxValue, "car price")]
        [Display(Name = "Price (in Euro):")]
        public decimal Price { get; set; }

        [RangeWithCustomFormat(CarKilometersMinValue, CarKilometersMaxValue, "car kilometers")]
        [Display(Name = "Mileage (in kilometers):")]
        public int Kilometers { get; set; }

        [Range(
            CarHorsepowerMinValue,
            CarHorsepowerMaxValue,
            ErrorMessage = "The car horsepower must be between {1} and {2}.")]
        [Display(Name = "Horsepower:")]
        public int Horsepower { get; set; }


        [Required(ErrorMessage = "The car country field is required.")]
        [StringLength(
            CarLocationCountryMaxLength,
            MinimumLength = CarLocationCountryMinLength,
            ErrorMessage = "The country name must be between {2} and {1} characters long.")]
        [Display(Name = "Car location - Country:")]
        public string LocationCountry { get; set; }

        [Required(ErrorMessage = "The car city field is required.")]
        [StringLength(
            CarLocationCityMaxLength,
            MinimumLength = CarLocationCityMinLength,
            ErrorMessage = "The city name must be between {2} and {1} characters long.")]
        [Display(Name = "Car location - City:")]
        public string LocationCity { get; set; }
        
        [Display(Name = "Images:")]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
