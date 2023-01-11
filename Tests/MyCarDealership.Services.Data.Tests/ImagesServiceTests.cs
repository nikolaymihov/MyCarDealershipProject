namespace MyCarDealership.Services.Data.Tests
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Xunit;
    using MyCarDealership.Data;
    using MyCarDealership.Services.Images;

    using static Data.Images;

    public class ImagesServiceTests
    {
        [Fact]
        public void SetCoverImagePropertyShouldSetTheIsCoverImagePropertyToTrue()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var imagesService = new ImagesService(dbContext);
            var testImage = ValidTestImage;

            testImage.IsCoverImage = false;

            dbContext.Images.Add(testImage);
            dbContext.SaveChanges();

            //Act
            imagesService.SetCoverImagePropertyAsync(testImage.Id).GetAwaiter().GetResult();

            //Assert
            Assert.True(testImage.IsCoverImage);
        }

        [Fact]
        public void RemoveCoverImagePropertyShouldSetTheIsCoverImagePropertyToFalse()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var imagesService = new ImagesService(dbContext);
            var testImage = ValidTestImage;

            testImage.IsCoverImage = true;

            dbContext.Images.Add(testImage);
            dbContext.SaveChanges();

            //Act
            imagesService.RemoveCoverImagePropertyAsync(testImage.Id).GetAwaiter().GetResult();

            //Assert
            Assert.False(testImage.IsCoverImage);
        }
    }
}