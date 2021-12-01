namespace MyCarDealershipProject.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Models.Cars;
    using Models.Posts;
    using Services.Cars;
    using Services.Posts;

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
            var postViewModel = new CreatePostInputModel();
            var car = new CreateCarInputModel
            {
                Categories = this.carsService.GetAllCategories(),
                FuelTypes = this.carsService.GetAllFuelTypes(),
                TransmissionTypes = this.carsService.GetAllTransmissionTypes(),
                CarExtras = this.carsService.GetAllCarExtras()
            };

            postViewModel.Car = car;

            return this.View(postViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreatePostInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Car.Categories = this.carsService.GetAllCategories();
                input.Car.FuelTypes = this.carsService.GetAllFuelTypes();
                input.Car.TransmissionTypes = this.carsService.GetAllTransmissionTypes();
                input.Car.CarExtras = this.carsService.GetAllCarExtras();

                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.postsService.CreateAsync(input, userId, $"{this.environment.WebRootPath}/images");

            return this.RedirectToAction("Index", "Home");
        }
    }
}
