namespace MyCarDealershipProject.Services.Posts
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Data.Models;

    public class PostsService : IPostsService
    {
        private readonly CarDealershipDbContext data;

        public PostsService(CarDealershipDbContext data)
        {
            this.data = data;
        }

        public async Task CreateAsync(Car car, string userId)
        {
            var post = new Post
            {
                Car = car,
                CreatorId = userId,
                PublishedOn = DateTime.UtcNow,
            };

            await this.data.Posts.AddAsync(post);
            await this.data.SaveChangesAsync();
        }
    }
}