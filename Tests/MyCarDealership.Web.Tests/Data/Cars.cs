namespace MyCarDealership.Web.Tests.Data
{
    using ViewModels.Cars;
    using MyCarDealership.Services.Cars.Models;

    using static Images;
    using static TestDataConstants;

    public static class Cars
    {
        public static CarFormInputModel ValidTestCarFormInputModel => new()
        {
            Make = TestMake,
            Model = TestModel,
            Description = TestDescription,
            CategoryId = 1,
            FuelTypeId = 1,
            TransmissionTypeId = 1,
            Year = TestYear,
            Kilometers = TestKilometers,
            Horsepower = TestHorsepower,
            Price = TestPrice,
            Images = GetTestImages(),
            LocationCountry = TestLocationCountry,
            LocationCity = TestLocationCity,
        };

        public static CarInListDTO ValidCarInListDTO => new()
        {
            Make = TestMake,
            Model = TestModel,
            Description = TestDescription,
            Year = TestYear,
            Kilometers = TestKilometers,
            Price = TestPrice,
            LocationCountry = TestLocationCountry,
            LocationCity = TestLocationCity,
        };

        public static SingleCarDTO ValidSingleCarDTO(int id) => new()
        {
            Id = id,
            Make = TestMake,
            Model = TestModel,
            Year = TestYear,
            Price = TestPrice,
            Description = TestDescription,
            Kilometers = TestKilometers,
            Horsepower = TestHorsepower,
            LocationCountry = TestLocationCountry,
            LocationCity = TestLocationCity,
        };

        public static CarByUserDTO ValidCarByUserDTO => new()
        {
            Make = TestMake,
            Model = TestModel,
            Year = TestYear,
            Price = TestPrice,
            CoverImage = TestCoverImage,
        };

        public static CarFormInputModelDTO ValidTestCarFormInputModelDTO => new()
        {
            Make = TestMake,
            Model = TestModel,
            Description = TestDescription,
            CategoryId = 1,
            FuelTypeId = 1,
            TransmissionTypeId = 1,
            Year = TestYear,
            Kilometers = TestKilometers,
            Horsepower = TestHorsepower,
            Price = TestPrice,
            Images = GetTestImages(),
            LocationCountry = TestLocationCountry,
            LocationCity = TestLocationCity,
        };
    }
}