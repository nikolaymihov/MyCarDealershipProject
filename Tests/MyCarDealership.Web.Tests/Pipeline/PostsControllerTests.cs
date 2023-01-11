namespace MyCarDealership.Web.Tests.Pipeline
{
    using System.Threading.Tasks;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using Xunit;
    using AutoMapper;
    using Controllers;
    using ViewModels.Posts;
    using MyTested.AspNetCore.Mvc;
    using MyCarDealership.Services.Cars;
    using MyCarDealership.Services.Posts;
    using MyCarDealership.Services.Posts.Models;
    using MyCarDealership.MapperConfigurations.Profiles;

    using static Data.Posts;
    using static Constants.WebConstants;

    public class PostsControllerTests
    {
        private readonly IMapper mapper;

        public PostsControllerTests()
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
        public void GetCreateShouldBeForAuthorizedUsersAndReturnViewWithCorrectModel()
        {
            MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithPath("/Posts/Create")
                    .WithUser())
                .To<PostsController>(c => c.Create())
                .Which()
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view.WithModelOfType<PostFormInputModel>());
        }

        [Fact]
        public async Task PostCreateWithValidModelShouldSavePostAndRedirect()
        {
            // Arrange
            var carsServiceMock = new Mock<ICarsService>();
            var postsServiceMock = new Mock<IPostsService>();
            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "1"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
            }, "mock"));
            
            var httpContext = new DefaultHttpContext() { User = user };
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessageKey"] = "Success!"
            };

            var postsController = new PostsController(carsServiceMock.Object, postsServiceMock.Object, webHostEnvironmentMock.Object, this.mapper)
            {
                TempData = tempData
            };

            postsController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            // Act
            var result = await postsController.Create(ValidTestPostFormInputModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Offer", redirectToActionResult.ActionName);
            Assert.NotNull(postsController.TempData[SuccessMessageKey]);
        }

        [Fact]
        public void GetSearchShouldReturnViewWithCorrectModel()
        {
            MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithPath("/Posts/Search"))
                .To<PostsController>(c => c.Search())
                .Which()
                .ShouldReturn()
                .View(view => view.WithModelOfType<SearchPostInputModel>());
        }

        [Fact]
        public void GetAllWithNoSearchModelAndDefaultPageNumberAndSortingShouldBeRoutedCorrectly()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Posts/All/1?sorting=0")
                .To<PostsController>(c => c.All(With.Any<SearchPostInputModel>(), 1, 0));
        }

        [Fact]
        public void GetAllWithDefaultPageNumberAndSortingShouldReturnDefaultViewWithCorrectModel()
        {
            // Arrange
            var carsServiceMock = new Mock<ICarsService>();
            var postsServiceMock = new Mock<IPostsService>();
            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            var searchPostInputModelMock = new Mock<SearchPostInputModel>();

            postsServiceMock.Setup(psm => psm.GetMatchingPosts(It.IsAny<SearchPostDTO>(), 0)).Returns(TenPublicPostInListDTOs());

            var postsController = new PostsController(carsServiceMock.Object, postsServiceMock.Object, webHostEnvironmentMock.Object, this.mapper);

            // Act
            var result = postsController.All(searchPostInputModelMock.Object, 1, 0);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PostsListViewModel>(viewResult.Model);
            Assert.Equal(10, model.PostsCount);
            Assert.Equal(1, model.PageNumber);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetOfferByIdShouldReturnViewWithCorrectModel(int postId)
        {
            // Arrange
            var carsServiceMock = new Mock<ICarsService>();
            var postsServiceMock = new Mock<IPostsService>();
            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            var principalMock = new Mock<ClaimsPrincipal>();

            principalMock.Setup(u => u.IsInRole(It.IsAny<string>())).Returns(false);
            principalMock.Setup(u => u.Identity.IsAuthenticated).Returns(false);
            postsServiceMock.Setup(psm => psm.GetSinglePostViewModelById(postId, true)).Returns(ValidSinglePostDTO(postId));

            var postsController = new PostsController(carsServiceMock.Object, postsServiceMock.Object, webHostEnvironmentMock.Object, this.mapper);
            var httpContext = new DefaultHttpContext() { User = principalMock.Object };

            postsController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            // Act
            var result = postsController.Offer(postId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<SinglePostViewModel>(viewResult.Model);
            Assert.Equal(postId, model.Car.Id);
        }

        [Fact]
        public void GetMineWithDefaultPageNumberAndSortingShouldReturnViewWithCorrectModel()
        {
            // Arrange
            var carsServiceMock = new Mock<ICarsService>();
            var postsServiceMock = new Mock<IPostsService>();
            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            var principalMock = new Mock<ClaimsPrincipal>();

            principalMock.Setup(u => u.FindFirst(It.IsAny<string>())).Returns(new Claim("name", "1"));

            postsServiceMock.Setup(psm => psm.GetPostsByUser(It.IsAny<string>(), 0)).Returns(TenPublicPostByUserDTOs);

            var postsController = new PostsController(carsServiceMock.Object, postsServiceMock.Object, webHostEnvironmentMock.Object, this.mapper);
            var httpContext = new DefaultHttpContext() { User = principalMock.Object };

            postsController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            // Act
            var result = postsController.Mine();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PostsByUserViewModel>(viewResult.Model);
            Assert.Equal(1, model.PageNumber);
            Assert.Equal(0, ((int)model.Sorting));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetEditByIdShouldReturnViewWithCorrectModel(int postId)
        {
            // Arrange
            var carsServiceMock = new Mock<ICarsService>();
            var postsServiceMock = new Mock<IPostsService>();
            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            var principalMock = new Mock<ClaimsPrincipal>();

            principalMock.Setup(u => u.IsInRole(It.IsAny<string>())).Returns(true);
            principalMock.Setup(u => u.FindFirst(It.IsAny<string>())).Returns(new Claim("name", "1"));

            postsServiceMock.Setup(psm => psm.GetPostFormInputModelById(postId)).Returns(ValidTestEditPostDTO);

            var postsController = new PostsController(carsServiceMock.Object, postsServiceMock.Object, webHostEnvironmentMock.Object, this.mapper);
            var httpContext = new DefaultHttpContext() { User = principalMock.Object };

            postsController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            // Act
            var result = postsController.Edit(postId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<EditPostViewModel>(viewResult.Model);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetDeleteByIdShouldReturnViewWithCorrectModel(int postId)
        {
            // Arrange
            var carsServiceMock = new Mock<ICarsService>();
            var postsServiceMock = new Mock<IPostsService>();
            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            var principalMock = new Mock<ClaimsPrincipal>();

            principalMock.Setup(u => u.IsInRole(It.IsAny<string>())).Returns(true);
            principalMock.Setup(u => u.FindFirst(It.IsAny<string>())).Returns(new Claim("name", "1"));

            postsServiceMock.Setup(psm => psm.GetBasicPostInformationById(postId)).Returns(ValidTestPostByUserDTO);

            var postsController = new PostsController(carsServiceMock.Object, postsServiceMock.Object, webHostEnvironmentMock.Object, this.mapper);
            var httpContext = new DefaultHttpContext() { User = principalMock.Object };

            postsController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            // Act
            var result = postsController.Delete(postId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PostByUserViewModel>(viewResult.Model);
        }
    }
}