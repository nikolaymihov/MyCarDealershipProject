namespace MyCarDealershipProject.Models.Posts
{
    using System.Collections.Generic;
    using Contracts;

    public class PostsByUserViewModel : PagingViewModel, ISortableModel
    {
        public IEnumerable<PostByUserViewModel> Posts { get; init; }

        public PostsSorting Sorting { get; set; }
    }
}