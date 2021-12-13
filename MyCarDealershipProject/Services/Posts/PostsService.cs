namespace MyCarDealershipProject.Services.Posts
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Data;
    using Data.Models;
    using Models.Cars;
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

        public IEnumerable<PostInListViewModel> GetAll()
        {
            var posts = this.data.Posts
                .OrderByDescending(p => p.Id)
                .Select(p => new PostInListViewModel()
                {
                    Car = new CarInListViewModel()
                    {
                        Id = p.Car.Id,
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        Description = p.Car.Description.Length <= 100 ? 
                                                        p.Car.Description 
                                                        : 
                                                        p.Car.Description.Substring(0, 100) + "...",
                        Price = p.Car.Price,
                        Year = p.Car.Year,
                        Kilometers = p.Car.Kilometers,
                        FuelType = p.Car.FuelType.Name,
                        TransmissionType = p.Car.TransmissionType.Name,
                        Category = p.Car.Category.Name,
                        LocationCity = p.Car.LocationCity,
                        LocationCountry = p.Car.LocationCountry,
                        CoverImage = "/images/cars/" + p.Car.Images.FirstOrDefault().Id + "." +
                                     p.Car.Images.FirstOrDefault().Extension,
                    },
                    PublishedOn = p.PublishedOn.Date.ToString(CultureInfo.InvariantCulture),
                }).ToList();

            return posts;
        }
    }
}