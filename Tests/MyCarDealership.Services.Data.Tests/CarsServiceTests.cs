namespace MyCarDealership.Services.Data.Tests
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;
    using AutoMapper;
    using MyCarDealership.Data;
    using MyCarDealership.Data.Models;
    using MyCarDealership.Services.Cars;
    using MyCarDealership.Services.Images;
    using MyCarDealership.MapperConfigurations.Profiles;

    using static Data.Cars;
    using static Data.Images;
    using static Data.TestDataConstants;

    public class CarsServiceTests
    {
        private readonly IMapper mapper;

        public CarsServiceTests()
        {
            if (this.mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new CarsProfile());
                    mc.AddProfile(new ImagesProfile());
                    mc.AddProfile(new PostsProfile());
                });

                IMapper mapper = mappingConfig.CreateMapper();

                this.mapper = mapper;
            }
        }

        [Fact]
        public void GetCarFromInputModelShouldPopulateAndReturnACarCorrectly()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var testSelectedExtras = new List<int>();
            var testFilesList = new List<IFormFile>();
            var testInputCar = ValidTestCarFormInputModelDTO;

            var fileMock = new Mock<IFormFile>();
            var content = "A Fake File";
            var fileName = "fake.jpg";
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);

            writer.Write(content);
            writer.Flush();
            memoryStream.Position = 0;
            fileMock.Setup(fm => fm.OpenReadStream()).Returns(memoryStream);
            fileMock.Setup(fm => fm.FileName).Returns(fileName);
            fileMock.Setup(fm => fm.Length).Returns(memoryStream.Length);

            var file = fileMock.Object;

            testFilesList.Add(file);
            testInputCar.Images = testFilesList;

            var imagesServiceMock = new Mock<IImagesService>();
            imagesServiceMock.Setup(ism => ism.UploadImageAsync(file, null, null)).ReturnsAsync(ValidTestImage);
            var carsService = new CarsService(dbContext, imagesServiceMock.Object, this.mapper);
            
            //Act
            var result = carsService.GetCarFromInputModelAsync(testInputCar, testSelectedExtras, null, null).GetAwaiter().GetResult();

            //Assert
            Assert.Equal(TestMake, result.Make);
            Assert.Equal(TestModel, result.Model);
            Assert.Equal(TestDescription, result.Description);
            Assert.Equal(TestIdNumber, result.CategoryId);
            Assert.Equal(TestIdNumber, result.FuelTypeId);
            Assert.Equal(TestIdNumber, result.TransmissionTypeId);
            Assert.Equal(TestYear, result.Year);
            Assert.Equal(TestIntValue, result.Kilometers);
            Assert.Equal(TestIntValue, result.Horsepower);
            Assert.Equal(TestIntValue, result.Price);
            Assert.Equal(TestLocationCountry, result.LocationCountry);
            Assert.Equal(TestLocationCity, result.LocationCity);
        }

        [Fact]
        public void UpdateCarDataFromInputModelShouldUpdateTheCarCorrectly()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var testSelectedExtras = new List<int>();
            var testDeletedImages = new List<string>();
            var testImagesList = new List<Image>();
            var testDbCar = ValidTestCar;

            testImagesList.Add(new Image
            {
                CarId = testDbCar.Id,
                CreatorId = TestImageId
            });

            testDbCar.Images = testImagesList;
            dbContext.Cars.Add(testDbCar);
            dbContext.SaveChanges();

            var imagesServiceMock = new Mock<IImagesService>();
            var carsService = new CarsService(dbContext, imagesServiceMock.Object, this.mapper);
            var updatedCar = ValidUpdatedCatTestModel;

            //Act
            carsService.UpdateCarDataFromInputModelAsync(1, updatedCar, testSelectedExtras, testDeletedImages, null, null, null).GetAwaiter().GetResult();

            //Assert
            Assert.Equal(UpdatedTestMake, testDbCar.Make);
            Assert.Equal(UpdatedTestModel, testDbCar.Model);
            Assert.Equal(UpdatedTestDescription, testDbCar.Description);
            Assert.Equal(UpdatedTestIdNumber, testDbCar.CategoryId);
            Assert.Equal(UpdatedTestIdNumber, testDbCar.FuelTypeId);
            Assert.Equal(UpdatedTestIdNumber, testDbCar.TransmissionTypeId);
            Assert.Equal(UpdatedTestYear, testDbCar.Year);
            Assert.Equal(UpdatedTestIntValue, testDbCar.Kilometers);
            Assert.Equal(UpdatedTestIntValue, testDbCar.Horsepower);
            Assert.Equal(UpdatedTestIntValue, testDbCar.Price);
            Assert.Equal(UpdatedTestLocationCountry, testDbCar.LocationCountry);
            Assert.Equal(UpdatedTestLocationCity, testDbCar.LocationCity);
        }


        [Fact]
        public void DeleteCarByIdShouldMarkTheCarAsDeletedWhenFound()
        {
            //Arrange
            var testCar = new Car
            {
                Id = TestIdNumber,
                IsDeleted = false,
            };

            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var imagesServiceMock = new Mock<IImagesService>();

            dbContext.Cars.Add(testCar);
            dbContext.SaveChanges();

            var carsService = new CarsService(dbContext, imagesServiceMock.Object, this.mapper);

            //Act
            carsService.DeleteCarByIdAsync(testCar.Id).GetAwaiter().GetResult();

            //Assert
            Assert.True(testCar.IsDeleted);
        }
    }
}