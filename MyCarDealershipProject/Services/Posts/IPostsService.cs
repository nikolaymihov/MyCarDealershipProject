namespace MyCarDealershipProject.Services.Posts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Data.Models;
    using Models;
    using Models.Posts;

    public interface IPostsService
    {
        Task<int> CreateAsync(CreatePostInputModel inputPost, Car car, string userId);

        IEnumerable<PostInListViewModel> GetMatchingPosts(SearchPostInputModel searchInputModel, PostsSorting sorting = PostsSorting.NewestFirst);
        
        IEnumerable<T> GetPostsByPage<T>(IEnumerable<T> posts, int page, int postsPerPage);

        IEnumerable<PostByUserViewModel> GetPostsByUser(string userId);

        SinglePostViewModel GetById(int id);

        IEnumerable<PostInLatestListViewModel> GetLatest(int count);
    }
}
