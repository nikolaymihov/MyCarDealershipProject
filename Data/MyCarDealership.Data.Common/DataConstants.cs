namespace MyCarDealership.Data.Common
{
    public class DataConstants
    {
        //Car constants
        public const int CarMakeMaxLength = 30;
        public const int CarMakeMinLength = 2;
        public const int CarModelMaxLength = 30;
        public const int CarModelMinLength = 2;
        public const int CarDescriptionMinLength = 20;
        public const int CarYearMinValue = 1950;
        public const int CarPriceMinValue = 1;
        public const int CarPriceMaxValue = 10000000;
        public const int CarKilometersMinValue = 0;
        public const int CarKilometersMaxValue = 2000000;
        public const int CarHorsepowerMinValue = 5;
        public const int CarHorsepowerMaxValue = 5000;
        public const int CarLocationCountryMaxLength = 20;
        public const int CarLocationCountryMinLength = 3;
        public const int CarLocationCityMaxLength = 30;
        public const int CarLocationCityMinLength = 3;

        //Post constants
        public const int PostSellerNameMaxLength = 30;
        public const int PostSellerNameMinLength = 2;
        public const int PostSellerPhoneNumberMaxLength = 20;
        public const int PostSellerPhoneNumberMinLength = 6;

        //Other data entities constants
        public const int CategoryNameMaxLength = 30;
        public const int ExtraNameMaxLength = 100;
        public const int ExtraTypeNameMaxLength = 20;
        public const int FuelTypeNameMaxLength = 40;
        public const int TransmissionTypeNameMaxLength = 30;
    }
}