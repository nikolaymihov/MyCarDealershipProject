namespace MyCarDealershipProject.Models.Cars
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class BaseCarInputModel
    {
        [Required]
        [StringLength(CarMakeMaxLength, MinimumLength = CarMakeMinLength)]
        [Display(Name = "Car make:")]
        public string Make { get; set; }

        [Required]
        [StringLength(CarModelMaxLength, MinimumLength = CarModelMinLength)]
        [Display(Name = "Car model:")]
        public string Model { get; set; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = CarDescriptionMinLength,
            ErrorMessage = "The car description must be at least {2} characters long.")]
        [Display(Name = "Car description:")]
        public string Description { get; set; }

        [Range(CarYearMinValue, CarYearMaxValue)]
        [Display(Name = "Car first registration:")]
        public int Year { get; set; }

        [Range(CarPriceMinValue, CarPriceMaxValue)]
        [Display(Name = "Car price:")]
        public decimal Price { get; set; }

        [Range(CarKilometersMinValue, CarKilometersMaxValue)]
        [Display(Name = "Car kilometers:")]
        public int Kilometers { get; set; }

        [Range(CarHorsepowerMinValue, CarHorsepowerMaxValue)]
        [Display(Name = "Car horsepower:")]
        public int Horsepower { get; set; }
    }
}
