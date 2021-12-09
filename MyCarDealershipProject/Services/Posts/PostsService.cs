namespace MyCarDealershipProject.Services.Posts
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Models.Posts;

    public class PostsService : IPostsService
    {
        private readonly CarDealershipDbContext data;

        public PostsService(CarDealershipDbContext data)
        {
            this.data = data;
        }

        public async Task CreateAsync(CreatePostInputModel inputPost, Car car, string userId)
        {
            var post = new Post
            {
                Car = car,
                CreatorId = userId,
                PublishedOn = DateTime.UtcNow,
                SellerName = inputPost.SellerName,
                SellerPhoneNumber = inputPost.SellerPhoneNumber
            };

            await this.data.Posts.AddAsync(post);
            await this.data.SaveChangesAsync();
        }
    }
}