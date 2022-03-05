namespace MyCarDealershipProject.Services.Posts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;
    using Data.Models;
    using Images.Models;

    public interface IPostsService
    {
        Task<int> CreateAsync(PostFormInputModelDTO inputPost, Car car, string userId);

        IEnumerable<PostInListDTO> GetMatchingPosts(SearchPostDTO searchInputModel, int sortingNumber);
        
        IEnumerable<T> GetPostsByPage<T>(IEnumerable<T> posts, int page, int postsPerPage);

        IEnumerable<PostByUserDTO> GetPostsByUser(string userId, int sortingNumber);

        SinglePostDTO GetSinglePostViewModelById(int postId);

        EditPostDTO GetPostFormInputModelById(int postId);

        IEnumerable<PostInLatestListDTO> GetLatest(int count);

        Task UpdateAsync(int postId, EditPostDTO input);

        IEnumerable<ImageInfoDTO> GetCurrentDbImagesForAPost(int postId);

        PostByUserDTO GetBasicPostInformationById(int postId);

        string GetPostCreatorId(int postId);

        Task DeletePostByIdAsync(int postId);
    }
}
