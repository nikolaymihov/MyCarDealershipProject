namespace MyCarDealershipProject.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models;
    using AutoMapper;
    using Models.Posts;
    using Services.Posts;
    using Services.Posts.Models;

    public class HomeController : Controller
    {
        private readonly IPostsService postsService;
        private readonly IMapper mapper;

        public HomeController(ILogger<HomeController> logger, IPostsService postsService, IMapper mapper)
        {
            this.postsService = postsService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var latestPostsDTO = this.postsService.GetLatest(5);
            var latestPostsViewModel = this.mapper.Map<IEnumerable<PostInLatestListDTO>, IEnumerable<PostInLatestListViewModel>>(latestPostsDTO);

            var latestPosts = new LatestPostsViewModel()
            {
                LatestPosts = latestPostsViewModel,
            };

            return this.View(latestPosts);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string code)
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier, StatusCode = code });
        }

        public IActionResult StatusCode(string code)
        {
            return this.RedirectToAction("Error", new { code });
        }
    }
}
