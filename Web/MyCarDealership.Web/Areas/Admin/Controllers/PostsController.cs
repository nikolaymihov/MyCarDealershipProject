namespace MyCarDealership.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using AutoMapper;
    using ViewModels.Posts;
    using Services.Posts;
    using Services.Posts.Models;

    using static AdminConstants;
    using static GlobalConstants.GlobalConstants;

    [Area(AdminAreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class PostsController : Controller
    {
        private const int PostsPerPage = 12;

        private readonly IPostsService postsService;
        private readonly IMapper mapper;

        public PostsController(IPostsService postsService, IMapper mapper)
        {
            this.postsService = postsService;
            this.mapper = mapper;
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var postsDTO = this.postsService.GetAllPostsBaseInfo(id, 12);
            var postsViewModel = this.mapper.Map<IEnumerable<BasePostInListDTO>, IEnumerable<PostInAdminAreaViewModel>>(postsDTO);
            var allPosts = this.postsService.GetAllPostsCount();

            var postsListViewModel = new PostsListAdminAreaViewModel()
            {
                PageNumber = id,
                PostsPerPage = PostsPerPage,
                PostsCount = allPosts,
                Posts = postsViewModel,
            };

            if (id > postsListViewModel.PagesCount)
            {
                return this.NotFound();
            }

            return this.View(postsListViewModel);
        }
        
        public async Task<IActionResult> ChangeVisibility(int id)
        {
            await this.postsService.ChangeVisibilityAsync(id);

            return RedirectToAction("All");
        }
    }
}