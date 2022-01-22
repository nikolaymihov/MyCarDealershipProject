namespace MyCarDealershipProject.Models.Posts
{
    using System.Collections.Generic;

    public class PostsListViewModel : PagingViewModel
    {
        public IEnumerable<PostInListViewModel> Posts { get; init; }

        public PostsSorting Sorting { get; set; }
    }
}