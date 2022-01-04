namespace MyCarDealershipProject.Services.Posts
{
    using System;
    using System.Linq;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal.Account.Manage;
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

        public IEnumerable<PostInListViewModel> GetAll(int page, int postsPerPage = 12)
        {
            var posts = this.data.Posts
                .OrderByDescending(p => p.Id)
                .Skip((page - 1) * postsPerPage).Take(postsPerPage) //page 1 --> skip 0 take 12, page 2 --> skip 12 take 12
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
                    PublishedOn = p.PublishedOn.Date == DateTime.UtcNow.Date ? 
                                                                "Today, " + p.PublishedOn.ToString("t", CultureInfo.InvariantCulture) 
                                                                : 
                                                                p.PublishedOn.ToString("d", CultureInfo.InvariantCulture),
                }).ToList();

            return posts;
        }

        public int GetCount()
        {
            return this.data.Posts.Count();
        }

        public SinglePostViewModel GetById(int id)
        {
            var post = this.data.Posts
                .Where(p => p.CarId == id)
                .Select(p => new SinglePostViewModel()
                {
                    Car = new SingleCarViewModel()
                    {
                        Id = p.Car.Id,
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        Description = p.Car.Description,
                        Price = p.Car.Price,
                        Year = p.Car.Year,
                        Kilometers = p.Car.Kilometers,
                        Horsepower = p.Car.Horsepower,
                        FuelType = p.Car.FuelType.Name,
                        TransmissionType = p.Car.TransmissionType.Name,
                        Category = p.Car.Category.Name,
                        LocationCity = p.Car.LocationCity,
                        LocationCountry = p.Car.LocationCountry,
                        ComfortExtras = p.Car.CarExtras.Where(ce => ce.Extra.TypeId == 1).Select(ce => ce.Extra.Name).ToList(),
                        SafetyExtras = p.Car.CarExtras.Where(ce => ce.Extra.TypeId == 2).Select(ce => ce.Extra.Name).ToList(),
                        OtherExtras = p.Car.CarExtras.Where(ce => ce.Extra.TypeId == 3).Select(ce => ce.Extra.Name).ToList(),
                        Images = p.Car.Images.Select(img => "/images/cars/" + img.Id + "." + img.Extension).ToList(),
                    },
                    PublishedOn = p.PublishedOn.ToString("d", CultureInfo.InvariantCulture),
                    SellerName = p.SellerName,
                    SellerPhoneNumber = p.SellerPhoneNumber
                })
                .FirstOrDefault();

            return post;
        }
    }
}