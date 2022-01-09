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
            var randomPosts = new RandomPostsViewModel()
            {
                RandomPosts = this.postsService.GetRandom(6),
            };

            return this.View(randomPosts);
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
