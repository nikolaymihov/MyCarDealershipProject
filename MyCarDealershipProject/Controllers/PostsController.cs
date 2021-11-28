namespace MyCarDealershipProject.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Models.Cars;
    using Models.Posts;

    public class PostsController : Controller
    {
        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreatePostInputModel {Car = new CreateCarInputModel()};

            return this.View(viewModel);
        }
    }
}
