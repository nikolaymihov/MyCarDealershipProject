namespace MyCarDealershipProject.Models.Posts
{
    using System.Collections.Generic;

    public class PostsByUserViewModel : PagingViewModel
    {
        public IEnumerable<PostByUserViewModel> Posts { get; init; }
    }
}