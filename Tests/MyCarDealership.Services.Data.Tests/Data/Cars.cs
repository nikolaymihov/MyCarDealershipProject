namespace MyCarDealership.Services.Data.Tests.Data
{
    using MyCarDealership.Data.Models;
    using MyCarDealership.Services.Cars.Models;

    using static TestDataConstants;

    public class Cars
    {
        public static CarFormInputModelDTO ValidTestCarFormInputModelDTO => new()
        {
            Make = TestMake,
            Model = TestModel,
            Description = TestDescription,
            CategoryId = TestIdNumber,
            FuelTypeId = TestIdNumber,
            TransmissionTypeId = TestIdNumber,
            Year = TestYear,
            Kilometers = TestIntValue,
            Horsepower = TestIntValue,
            Price = TestIntValue,
            LocationCountry = TestLocationCountry,
            LocationCity = TestLocationCity,
        };

        public static Car ValidTestCar => new()
        {
            Id = TestIdNumber,
            Make = TestMake,
            Model = TestModel,
            Description = TestDescription,
            CategoryId = TestIdNumber,
            FuelTypeId = TestIdNumber,
            TransmissionTypeId = TestIdNumber,
            Year = TestYear,
            Kilometers = TestIntValue,
            Horsepower = TestIntValue,
            Price = TestIntValue,
            LocationCountry = TestLocationCountry,
            LocationCity = TestLocationCity,
        };

        public static CarFormInputModelDTO ValidUpdatedCatTestModel => new()
        {
            Make = UpdatedTestMake,
            Model = UpdatedTestModel,
            Description = UpdatedTestDescription,
            CategoryId = UpdatedTestIdNumber,
            FuelTypeId = UpdatedTestIdNumber,
            TransmissionTypeId = UpdatedTestIdNumber,
            Year = UpdatedTestYear,
            Kilometers = UpdatedTestIntValue,
            Horsepower = UpdatedTestIntValue,
            Price = UpdatedTestIntValue,
            LocationCountry = UpdatedTestLocationCountry,
            LocationCity = UpdatedTestLocationCity,
        };

        public static SearchCarInputModelDTO ValidSearchCarInputModelDTO => new()
        {
            TextSearchTerm = TestMake,
            FromYear = TestYear,
        };
    }
}