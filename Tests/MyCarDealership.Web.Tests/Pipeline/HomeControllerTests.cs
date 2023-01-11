namespace MyCarDealership.Web.Tests.Pipeline
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using ViewModels;
    using Controllers;
    using ViewModels.Posts;

    using static Data.Posts;

    public class HomeControllerTests
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index())
                .Which(controller => controller
                    .WithData(TenPublicPosts()))
                .ShouldReturn()
                .View(view => 
                      view.WithModelOfType<LatestPostsViewModel>());
        }

        [Fact]
        public void PrivacyShouldReturnView()
        {
            MyMvc
                .Pipeline()
                .ShouldMap("/Home/Privacy")
                .To<HomeController>(c => c.Privacy())
                .Which()
                .ShouldReturn()
                .View();
        }
        
        [Theory]
        [InlineData("401")]
        [InlineData("404")]
        [InlineData("500")]
        public void ErrorShouldReturnViewWithCorrectModelAndStatusCode(string code)
            => MyMvc
                .Pipeline()
                .ShouldMap($"/Home/Error?code={code}")
                .To<HomeController>(c => c.Error(code))
                .Which()
                .ShouldReturn()
                .View(view => 
                      view.WithModelOfType<ErrorViewModel>()
                          .Passing(vm => vm.StatusCode == code));


        [Fact]
        public void ErrorWithoutCodeParameterShouldReturnViewWithCorrectModel()
            => MyMvc
                .Pipeline()
                .ShouldMap($"/Home/Error")
                .To<HomeController>(c => c.Error(With.No<string>()))
                .Which()
                .ShouldReturn()
                .View(view =>
                    view.WithModelOfType<ErrorViewModel>());

        [Theory]
        [InlineData("401")]
        [InlineData("404")]
        [InlineData("500")]
        public void StatusCodeShouldRedirectToErrorWithCorrectStatusCode(string code)
            => MyMvc
                .Pipeline()
                .ShouldMap($"/Home/StatusCode?code={code}")
                .To<HomeController>(c => c.StatusCode(code))
                .Which()
                .ShouldReturn()
                .Redirect(redirect =>
                          redirect.To<HomeController>(c => c.Error(code)));
    }
}