namespace MyCarDealership.Services.Posts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;
    using Data.Models;
    using Images.Models;

    public interface IPostsService
    {
        Task<int> CreateAsync(PostFormInputModelDTO inputPost, Car car, string userId, bool isPublic);

        IEnumerable<PostInListDTO> GetMatchingPosts(SearchPostDTO searchInputModel, int sortingNumber);

        IEnumerable<BasePostInListDTO> GetAllPostsBaseInfo(int page, int postsPerPage);

        int GetAllPostsCount();

        IEnumerable<T> GetPostsByPage<T>(IEnumerable<T> posts, int page, int postsPerPage);

        IEnumerable<PostByUserDTO> GetPostsByUser(string userId, int sortingNumber);

        SinglePostDTO GetSinglePostViewModelById(int postId, bool publicOnly = true);

        EditPostDTO GetPostFormInputModelById(int postId);

        IEnumerable<PostInLatestListDTO> GetLatest(int count);

        Task UpdateAsync(int postId, EditPostDTO input, bool isPublic);

        Task ChangeVisibilityAsync(int postId);

        IEnumerable<ImageInfoDTO> GetCurrentDbImagesForAPost(int postId);

        PostByUserDTO GetBasicPostInformationById(int postId);

        string GetPostCreatorId(int postId);

        Task DeletePostByIdAsync(int postId);
    }
}
