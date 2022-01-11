namespace MyCarDealershipProject.Services.Posts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Data.Models;
    using Models.Posts;

    public interface IPostsService
    {
        Task CreateAsync(CreatePostInputModel inputPost, Car car, string userId);

        IEnumerable<PostInListViewModel> GetAll(int page, int postsPerPage = 12);

        int GetCount();

        SinglePostViewModel GetById(int id);

        IEnumerable<PostInLatestListViewModel> GetLatest(int count);
    }
}
