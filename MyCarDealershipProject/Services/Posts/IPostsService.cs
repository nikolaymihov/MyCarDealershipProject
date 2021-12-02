namespace MyCarDealershipProject.Services.Posts
{
    using System.Threading.Tasks;
    using Data.Models;

    public interface IPostsService
    {
        Task CreateAsync(Car car, string userId);
    }
}
