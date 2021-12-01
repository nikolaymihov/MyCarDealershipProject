namespace MyCarDealershipProject.Services.Posts
{
    using System.Threading.Tasks;
    using Models.Posts;

    public interface IPostsService
    {
        Task CreateAsync(CreatePostInputModel input, string userId, string imagePath);
    }
}
