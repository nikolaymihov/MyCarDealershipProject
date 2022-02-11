namespace MyCarDealershipProject.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models;
    using Models.Posts;
    using Services.Posts;

    public class HomeController : Controller
    {
        private readonly IPostsService postsService;

        public HomeController(ILogger<HomeController> logger, IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public IActionResult Index()
        {
            var latestPosts = new LatestPostsViewModel()
            {
                LatestPosts = this.postsService.GetLatest(5),
            };

            return this.View(latestPosts);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
