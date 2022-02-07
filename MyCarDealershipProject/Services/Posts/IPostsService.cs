namespace MyCarDealershipProject.Services.Posts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;
    using Data.Models;
    using Models.Posts;
    using Models.Images;

    public interface IPostsService
    {
        Task<int> CreateAsync(PostFormInputModel inputPost, Car car, string userId);

        IEnumerable<PostInListViewModel> GetMatchingPosts(SearchPostInputModel searchInputModel, PostsSorting sorting = PostsSorting.NewestFirst);
        
        IEnumerable<T> GetPostsByPage<T>(IEnumerable<T> posts, int page, int postsPerPage);

        IEnumerable<PostByUserViewModel> GetPostsByUser(string userId, PostsSorting sorting = PostsSorting.NewestFirst);

        SinglePostViewModel GetSinglePostViewModelById(int postId);

        EditPostViewModel GetPostFormInputModelById(int postId);

        IEnumerable<PostInLatestListViewModel> GetLatest(int count);

        Task UpdateAsync(int postId, EditPostViewModel input);

        IEnumerable<ImageInfoViewModel> GetCurrentDbImagesForAPost(int postId);
    }
}
