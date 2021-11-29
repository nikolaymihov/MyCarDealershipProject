namespace MyCarDealershipProject.Models.Cars
{
    using System.Globalization;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class BaseCarInputModel
    {
        [Required]
        [StringLength(
            CarMakeMaxLength, 
            MinimumLength = CarMakeMinLength,
            ErrorMessage = "The car make must be between {2} and {1} characters long.")]
        [Display(Name = "Car make:")]
        public string Make { get; set; }

        [Required]
        [StringLength(
            CarModelMaxLength, 
            MinimumLength = CarModelMinLength,
            ErrorMessage = "The car model must be between {2} and {1} characters long.")]
        [Display(Name = "Car model:")]
        public string Model { get; set; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = CarDescriptionMinLength,
            ErrorMessage = "The car description must be at least {2} characters long.")]
        [Display(Name = "Car description:")]
        public string Description { get; set; }

        [Range(
            CarYearMinValue, 
            CarYearMaxValue, 
            ErrorMessage = "The car year must be between {1} and {2}.")]
        [Display(Name = "Car first registration:")]
        public int Year { get; set; }

        [Range(
            CarPriceMinValue, 
            CarPriceMaxValue, 
            ErrorMessage = "The car price must be between {1} and {2}.")]
        [Display(Name = "Car price:")]
        public decimal Price { get; set; }

        [Range(
            CarKilometersMinValue, 
            CarKilometersMaxValue,
            ErrorMessage = "The car kilometers must be between {1} and {2}.")]
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
