namespace MyCarDealershipProject.Models.Cars
{
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.ValidationAttributes;
    using static Data.DataConstants;

    public class BaseCarInputModel
    {
        [Required(ErrorMessage = "The car make field is required.")]
        [StringLength(
            CarMakeMaxLength, 
            MinimumLength = CarMakeMinLength,
            ErrorMessage = "The car make must be between {2} and {1} characters long.")]
        [Display(Name = "Car make:")]
        public string Make { get; set; }

        [Required(ErrorMessage = "The car model field is required.")]
        [StringLength(
            CarModelMaxLength, 
            MinimumLength = CarModelMinLength,
            ErrorMessage = "The car model must be between {2} and {1} characters long.")]
        [Display(Name = "Car model:")]
        public string Model { get; set; }

        [Required(ErrorMessage = "The car description field is required.")]
        [StringLength(
            int.MaxValue,
            MinimumLength = CarDescriptionMinLength,
            ErrorMessage = "The car description must be at least {2} characters long.")]
        [Display(Name = "Car description:")]
        public string Description { get; set; }

        [RangeUntilCurrentYear(
            CarYearMinValue, 
            ErrorMessage = "The car year must be between {1} and {2}.")]
        [Display(Name = "Car year:")]
        public int Year { get; set; }

        [RangeWithCustomFormat(CarPriceMinValue, CarPriceMaxValue, "car price")]
        [Display(Name = "Car price:")]
        public decimal Price { get; set; }

        [RangeWithCustomFormat(CarKilometersMinValue, CarKilometersMaxValue, "car kilometers")]
        [Display(Name = "Car kilometers:")]
        public int Kilometers { get; set; }

        [Range(
            CarHorsepowerMinValue, 
            CarHorsepowerMaxValue,
            ErrorMessage = "The car horsepower must be between {1} and {2}.")]
        [Display(Name = "Car horsepower:")]
        public int Horsepower { get; set; }
    }
}
