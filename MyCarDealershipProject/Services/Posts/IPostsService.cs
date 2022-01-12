namespace MyCarDealershipProject.Services.Posts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Data.Models;
    using Models.Posts;

    public interface IPostsService
    {
        Task CreateAsync(CreatePostInputModel inputPost, Car car, string userId);

        IEnumerable<PostInListViewModel> GetMatchingPosts(SearchPostInputModel searchInputModel);
        
        IEnumerable<PostInListViewModel> GetPostsByPage(IEnumerable<PostInListViewModel> posts, int page, int postsPerPage = 12);

        SinglePostViewModel GetById(int id);

        IEnumerable<PostInLatestListViewModel> GetLatest(int count);
    }
}
