namespace MyCarDealershipProject.Services.Posts
{
    using System.Threading.Tasks;
    using Data.Models;
    using Models.Posts;

    public interface IPostsService
    {
        Task CreateAsync(CreatePostInputModel inputPost, Car car, string userId);
    }
}
