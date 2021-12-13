namespace MyCarDealershipProject.Models.Posts
{
    using System.Collections.Generic;

    public class PostsListViewModel
    {
        public IEnumerable<PostInListViewModel> Posts { get; init; }
    }
}