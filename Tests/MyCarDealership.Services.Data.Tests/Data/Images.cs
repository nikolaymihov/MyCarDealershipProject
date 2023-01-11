namespace MyCarDealership.Services.Data.Tests.Data
{
    using MyCarDealership.Data.Models;

    using static TestDataConstants;

    public class Images
    {
        public static Image ValidTestImage => new()
        {
            Id = TestImageId,
            CarId = TestIdNumber,
            CreatorId = TestImageId,
        };
    }
}