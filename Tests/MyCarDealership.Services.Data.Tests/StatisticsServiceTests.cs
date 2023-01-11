namespace MyCarDealership.Services.Data.Tests
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Xunit;
    using MyCarDealership.Data;
    using MyCarDealership.Data.Models;
    using MyCarDealership.Services.Statistics;
    using MyCarDealership.Services.Statistics.Models;

    public class StatisticsServiceTests
    {
        [Fact]
        public void TotalWithValidDataShouldReturnCorrectModelAndCounts()
        {
            //Arrange
            var testUser = new ApplicationUser
            {
                FullName = "Test user"
            };

            var testPost = new Post
            {
                PublishedOn = DateTime.UtcNow,
                IsPublic = true,
                IsDeleted = false,
                SellerName = "Test seller",
                SellerPhoneNumber = "Test phone number"
            };

            var testCategory = new Category
            {
                Name = "Saloon"
            };

            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);

            dbContext.Posts.Add(testPost);
            dbContext.Users.Add(testUser);
            dbContext.Categories.Add(testCategory);
            dbContext.SaveChanges();

            var statisticsService = new StatisticsService(dbContext);
            
            //Act
            var result = statisticsService.Total();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<StatisticsServiceModel>(result);
            Assert.Equal(1, result.TotalUsers);
            Assert.Equal(1, result.TotalPosts);
            Assert.Equal(1, result.TotalCategories);
        }

        [Fact]
        public void TotalWithEmptyDatabaseShouldReturnCorrectModelAndCounts()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var statisticsService = new StatisticsService(dbContext);
            
            //Act
            var result = statisticsService.Total();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<StatisticsServiceModel>(result);
            Assert.Equal(0, result.TotalUsers);
            Assert.Equal(0, result.TotalPosts);
            Assert.Equal(0, result.TotalCategories);
        }
    }
}
