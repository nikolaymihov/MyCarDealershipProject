namespace MyCarDealershipProject.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Models.Cars;
    using Models.Posts;
    using Services.Cars;

    public class PostsController : Controller
    {
        private readonly ICarService carsService;

        public PostsController(ICarService carsService)
        {
            this.carsService = carsService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var postViewModel = new CreatePostInputModel();
            var car = new CreateCarInputModel
            {
                Categories = this.carsService.GetAllCategories()
            };

            postViewModel.Car = car;

            return this.View(postViewModel);
        }
    }
}
