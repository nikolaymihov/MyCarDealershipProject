namespace MyCarDealership.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;
    using MyCarDealership.Data;
    using MyCarDealership.Services.Cars;
    using MyCarDealership.Services.Posts;
    using MyCarDealership.Services.Images;
    using MyCarDealership.Services.Cars.Models;
    using MyCarDealership.Services.Posts.Models;

    using static Data.Cars;
    using static Data.Posts;
    using static Data.TestDataConstants;

    public class PostsServiceTests
    {
        [Fact]
        public void CreateWithPublicPostShouldCreateANewPublicPostAndSaveItInTheDatabase()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var carsServiceMock = new Mock<ICarsService>();
            var imagesServiceMock = new Mock<IImagesService>();
            var postsService = new PostsService(dbContext, carsServiceMock.Object, imagesServiceMock.Object);
            var testCar = ValidTestCar;
            var testInputPost = ValidTestPostFormInputModelDTO;
            var isPublic = true;

            //Act
            var createdPostId = postsService.CreateAsync(testInputPost, testCar, TestUserId, isPublic).GetAwaiter().GetResult();
            var createdPost = dbContext.Posts.AsQueryable().FirstOrDefaultAsync(p => p.Id == createdPostId).Result;

            //Assert
            Assert.True(createdPost.IsPublic);
            Assert.Equal(testInputPost.SellerName, createdPost.SellerName);
            Assert.Equal(testInputPost.SellerPhoneNumber, createdPost.SellerPhoneNumber);
            Assert.Equal(TestUserId, createdPost.CreatorId);
            Assert.Equal(testCar.Id, createdPost.Car.Id);
            Assert.Equal(testCar.Make, createdPost.Car.Make);
            Assert.Equal(testCar.Model, createdPost.Car.Model);
        }

        [Fact]
        public void GetPostsByUserShouldReturnCorrrectModel()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var carsServiceMock = new Mock<ICarsService>();
            var imagesServiceMock = new Mock<IImagesService>();
            var postsService = new PostsService(dbContext, carsServiceMock.Object, imagesServiceMock.Object);
            var testPost = ValidTestPublicPost;
            var sortingNumber = 0;

            dbContext.Posts.Add(testPost);
            dbContext.SaveChanges();

            //Act
            var result = postsService.GetPostsByUser(TestUserId, sortingNumber);

            //Assert
            Assert.Single(result);
            Assert.IsAssignableFrom<IEnumerable<PostByUserDTO>>(result);
        }

        [Fact]
        public void GetAllPostsBaseInfoShouldReturnCorrectMode()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var carsServiceMock = new Mock<ICarsService>();
            var imagesServiceMock = new Mock<IImagesService>();
            var postsService = new PostsService(dbContext, carsServiceMock.Object, imagesServiceMock.Object);
            var testPost = ValidTestPublicPost;
            var pageNumber = 1;
            var postsPerPage = 1;

            dbContext.Posts.Add(testPost);
            dbContext.SaveChanges();

            //Act
            var result = postsService.GetAllPostsBaseInfo(pageNumber, postsPerPage);

            //Assert
            Assert.Single(result);
            Assert.IsAssignableFrom<IEnumerable<BasePostInListDTO>>(result);
        }

        [Fact]
        public void GetAllPostsCountShouldReturnCorrectCount()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var carsServiceMock = new Mock<ICarsService>();
            var imagesServiceMock = new Mock<IImagesService>();
            var postsService = new PostsService(dbContext, carsServiceMock.Object, imagesServiceMock.Object);
            var testPost = ValidTestPublicPost;

            dbContext.Posts.Add(testPost);
            dbContext.SaveChanges();

            //Act
            var result = postsService.GetAllPostsCount();

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void GetAllPostsCountWithEmptyDatabaseShouldReturnCorrectCount()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var carsServiceMock = new Mock<ICarsService>();
            var imagesServiceMock = new Mock<IImagesService>();
            var postsService = new PostsService(dbContext, carsServiceMock.Object, imagesServiceMock.Object);

            //Act
            var result = postsService.GetAllPostsCount();

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void UpdateWithValidInputModelShouldUpdateThePostCorrectly()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var carsServiceMock = new Mock<ICarsService>();
            var imagesServiceMock = new Mock<IImagesService>();
            var postsService = new PostsService(dbContext, carsServiceMock.Object, imagesServiceMock.Object);
            var testPost = ValidTestPublicPost;

            dbContext.Posts.Add(testPost);
            dbContext.SaveChanges();

            var updatedPost = ValidEditPostDTO;
            var isPublic = true;

            //Act
            postsService.UpdateAsync(testPost.Id, updatedPost, isPublic).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(testPost.ModifiedOn);
            Assert.True(testPost.IsPublic);
            Assert.Equal(UpdatedTestPostSellerName, testPost.SellerName);
            Assert.Equal(UpdatedTestPostSellerPhoneNumber, testPost.SellerPhoneNumber);
        }

        [Fact]
        public void UpdateWithInvalidPostIdShouldThrowAnExcpetion()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var carsServiceMock = new Mock<ICarsService>();
            var imagesServiceMock = new Mock<IImagesService>();
            var postsService = new PostsService(dbContext, carsServiceMock.Object, imagesServiceMock.Object);
            var testPost = ValidTestPublicPost;

            dbContext.Posts.Add(testPost);
            dbContext.SaveChanges();

            var updatedPost = ValidEditPostDTO;
            var isPublic = true;
            var wrongId = testPost.Id + 1;

            //Act
            var exception = Record.Exception(() => postsService.UpdateAsync(wrongId, updatedPost, isPublic).GetAwaiter().GetResult());

            //Assert
            Assert.IsType<Exception>(exception);
        }

        [Fact]
        public void ChangeVisibilityWithPublicPostShouldCorrectlyMakeThePostNonPublic()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var carsServiceMock = new Mock<ICarsService>();
            var imagesServiceMock = new Mock<IImagesService>();
            var postsService = new PostsService(dbContext, carsServiceMock.Object, imagesServiceMock.Object);
            var testPost = ValidTestPublicPost;

            dbContext.Posts.Add(testPost);
            dbContext.SaveChanges();

            //Act
            postsService.ChangeVisibilityAsync(testPost.Id).GetAwaiter().GetResult();

            //Assert
            Assert.False(testPost.IsPublic);
        }

        [Fact]
        public void ChangeVisibilityWithNonPublicPostShouldCorrectlyMakeThePostPublic()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var carsServiceMock = new Mock<ICarsService>();
            var imagesServiceMock = new Mock<IImagesService>();
            var postsService = new PostsService(dbContext, carsServiceMock.Object, imagesServiceMock.Object);
            var testPost = ValidTestPublicPost;

            testPost.IsPublic = false;

            dbContext.Posts.Add(testPost);
            dbContext.SaveChanges();

            //Act
            postsService.ChangeVisibilityAsync(testPost.Id).GetAwaiter().GetResult();

            //Assert
            Assert.True(testPost.IsPublic);
        }

        [Fact]
        public void GetBasicPostInformationByIdShouldReturnCorrectModel()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var carsServiceMock = new Mock<ICarsService>();
            var imagesServiceMock = new Mock<IImagesService>();
            var postsService = new PostsService(dbContext, carsServiceMock.Object, imagesServiceMock.Object);
            var testPost = ValidTestPublicPost;

            dbContext.Posts.Add(testPost);
            dbContext.SaveChanges();

            //Act
            var result = postsService.GetBasicPostInformationById(testPost.Id);

            //Assert
            Assert.IsAssignableFrom<PostByUserDTO>(result);
            Assert.IsAssignableFrom<CarByUserDTO>(result.Car);
            Assert.Equal(testPost.Car.Id, result.Car.Id);
            Assert.Equal(testPost.Car.Make, result.Car.Make);
            Assert.Equal(testPost.Car.Model, result.Car.Model);
            Assert.Equal(testPost.Car.Price, result.Car.Price);
            Assert.Equal(testPost.Car.Year, result.Car.Year);
        }

        [Fact]
        public void GetPostCreatorIdShouldReturnCorrectUserIdWhenThePostExists()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var carsServiceMock = new Mock<ICarsService>();
            var imagesServiceMock = new Mock<IImagesService>();
            var postsService = new PostsService(dbContext, carsServiceMock.Object, imagesServiceMock.Object);
            var testPost = ValidTestPublicPost;

            dbContext.Posts.Add(testPost);
            dbContext.SaveChanges();

            //Act
            var result = postsService.GetPostCreatorId(testPost.Id);

            //Assert
            Assert.Equal(testPost.CreatorId, result);
        }

        [Fact]
        public void GetPostCreatorIdShouldReturnNullWhenThePostDoesNotExists()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var carsServiceMock = new Mock<ICarsService>();
            var imagesServiceMock = new Mock<IImagesService>();
            var postsService = new PostsService(dbContext, carsServiceMock.Object, imagesServiceMock.Object);
            var testPost = ValidTestPublicPost;

            //Act
            var result = postsService.GetPostCreatorId(testPost.Id);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeletePostByIdShouldMarkThePostAsDeletedAndNonPublic()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<CarDealershipDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new CarDealershipDbContext(optionsBuilder.Options);
            var carsServiceMock = new Mock<ICarsService>();
            var imagesServiceMock = new Mock<IImagesService>();
            var postsService = new PostsService(dbContext, carsServiceMock.Object, imagesServiceMock.Object);
            var testPost = ValidTestPublicPost;

            dbContext.Posts.Add(testPost);
            dbContext.SaveChanges();

            //Act
            postsService.DeletePostByIdAsync(testPost.Id).GetAwaiter().GetResult();

            //Assert
            Assert.True(testPost.IsDeleted);
            Assert.False(testPost.IsPublic);
        }
    }
}