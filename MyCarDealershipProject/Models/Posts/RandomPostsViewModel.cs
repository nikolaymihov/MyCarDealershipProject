namespace MyCarDealershipProject.Models.Posts
{
    using System.Collections.Generic;

    public class RandomPostsViewModel
    {
        public IEnumerable<PostInRandomListViewModel> RandomPosts { get; init; }
    }
}