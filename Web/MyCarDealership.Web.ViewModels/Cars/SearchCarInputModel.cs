namespace MyCarDealership.Web.ViewModels.Cars
{
    using System.ComponentModel.DataAnnotations;
    using CustomAttributes.ValidationAttributes;

    using static Data.Common.DataConstants;

    public class SearchCarInputModel : BaseCarInputModel
    {
        [Display(Name = "Make, model or/and specification:")]
        public string TextSearchTerm { get; set; }

        [RangeUntilCurrentYear(
            CarYearMinValue,
            ErrorMessage = "The car year must be between {1} and {2}.")]
        public int? FromYear { get; set; }

        [RangeUntilCurrentYear(
            CarYearMinValue,
            ErrorMessage = "The car year must be between {1} and {2}.")]
        public int? ToYear { get; set; }

        [Range(
            CarHorsepowerMinValue,
            CarHorsepowerMaxValue,
            ErrorMessage = "The car horsepower must be between {1} and {2}.")]
        public int? MinHorsepower { get; set; }

        [Range(
            CarHorsepowerMinValue,
            CarHorsepowerMaxValue,
            ErrorMessage = "The car horsepower must be between {1} and {2}.")]
        public int? MaxHorsepower { get; set; }

        [RangeWithCustomFormat(CarPriceMinValue, CarPriceMaxValue, "car price")]
        [Display(Name = "Minimum price (in Euro):")]
        public decimal? MinPrice { get; set; }

        [RangeWithCustomFormat(CarPriceMinValue, CarPriceMaxValue, "car price")]
        [Display(Name = "Maximum price (in Euro):")]
        public decimal? MaxPrice { get; set; }
    }
}