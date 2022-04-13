﻿namespace MyCarDealership.Services.Posts
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

        IEnumerable<BasePostInListDTO> GetAllPostsBaseInfo(int page, int postsPerPage);

        int GetAllPostsCount();

        IEnumerable<T> GetPostsByPage<T>(IEnumerable<T> posts, int page, int postsPerPage);

        IEnumerable<PostByUserDTO> GetPostsByUser(string userId, int sortingNumber);

        SinglePostDTO GetSinglePostViewModelById(int postId, bool publicOnly = true);

        EditPostDTO GetPostFormInputModelById(int postId, bool publicOnly = true);

        IEnumerable<PostInLatestListDTO> GetLatest(int count);

        Task UpdateAsync(int postId, EditPostDTO input);

        Task ChangeVisibilityAsync(int postId);

        IEnumerable<ImageInfoDTO> GetCurrentDbImagesForAPost(int postId);

        PostByUserDTO GetBasicPostInformationById(int postId, bool publicOnly = true);

        string GetPostCreatorId(int postId);

        Task DeletePostByIdAsync(int postId);
    }
}