namespace MyCarDealershipProject.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Authorization;
    using Models;
    using Models.Cars;
    using Models.Posts;
    using Services.Cars;
    using Services.Posts;
    using Infrastructure;

    public class PostsController : Controller
    {
        private readonly ICarsService carsService;
        private readonly IPostsService postsService;
        private readonly IWebHostEnvironment environment;
        
        public PostsController(ICarsService carsService, IPostsService postsService, IWebHostEnvironment environment)
        {
            this.carsService = carsService;
            this.postsService = postsService;
            this.environment = environment;
        }

        [Authorize]
        public IActionResult Create()
        {
            var createPostInputModel = new CreatePostInputModel();
            var createCarInputModel = new CreateCarInputModel();

            this.carsService.FillBaseInputCarProperties(createCarInputModel);

            createPostInputModel.Car = createCarInputModel;

            return this.View(createPostInputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreatePostInputModel input)
        {
            var inputCar = input.Car;

            if (!this.ModelState.IsValid)
            {
                this.carsService.FillBaseInputCarProperties(inputCar);
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var selectedExtrasIds = input.SelectedExtrasIds.ToList();
            var imagePath = $"{this.environment.WebRootPath}/images";
            
            try
            { 
                var car = await this.carsService.GetCarFromInputModel(inputCar, selectedExtrasIds, userId, imagePath);
                await this.postsService.CreateAsync(input, car, userId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("CustomError", ex.Message);
                this.carsService.FillBaseInputCarProperties(inputCar);
                return this.View(input);
            }

            TempData[GlobalConstants.GlobalSuccessMessageKey] = "Your car post was added successfully!";

            return this.RedirectToAction("All");
        }

        public IActionResult Search()
        {
            var searchPostInputModel = new SearchPostInputModel(); 
            var searchCarInputModel = new SearchCarInputModel();
            
            this.carsService.FillBaseInputCarProperties(searchCarInputModel);
            
            searchPostInputModel.Car = searchCarInputModel;
            
            return this.View(searchPostInputModel);
        }

        public IActionResult All(SearchPostInputModel searchPostInputModel, int id = 1, int sorting = 0)
        {
            try
            {
                if (id <= 0)
                {
                    return this.NotFound();
                }

                const int PostsPerPage = 12;
                
                var matchingPosts = this.postsService.GetMatchingPosts(searchPostInputModel, (PostsSorting)sorting).ToList();

                var postsListViewModel = new PostsListViewModel()
                {
                    PageNumber = id,
                    PostsPerPage = PostsPerPage,
                    PostsCount = matchingPosts.Count(),
                    Posts = this.postsService.GetPostsByPage(matchingPosts, id, PostsPerPage),
                };

                if (sorting > 0)
                {
                    Console.WriteLine();
                }

                if (id > postsListViewModel.PagesCount)
                {
                    return this.NotFound();
                }

                return this.View(postsListViewModel);
            }
            catch (Exception ex)
            {
                this.TempData[GlobalConstants.GlobalErrorMessageKey] = ex.Message;
                this.carsService.FillBaseInputCarProperties(searchPostInputModel.Car);
                return this.View("Search", searchPostInputModel);
            }
        }

        public IActionResult Offer(int id)
        {
            var singlePostData = this.postsService.GetById(id);

            return this.View(singlePostData);
        }
    }
}
