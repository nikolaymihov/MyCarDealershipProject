namespace MyCarDealership.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class PostsListAdminAreaViewModel : PagingViewModel
    {
        public IEnumerable<PostInAdminAreaViewModel> Posts { get; init; }
    }
}