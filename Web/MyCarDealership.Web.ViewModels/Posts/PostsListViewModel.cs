namespace MyCarDealership.Web.ViewModels.Posts
{
    using System.Collections.Generic;
    using Contracts;

    public class PostsListViewModel : PagingViewModel, ISortableModel
    {
        public IEnumerable<PostInListViewModel> Posts { get; init; }

        public PostsSorting Sorting { get; set; }
    }
}