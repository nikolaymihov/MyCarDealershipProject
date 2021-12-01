namespace MyCarDealershipProject.Services.Posts
{
    using System;
    using Data;
    using AutoMapper;
    using Models.Posts;
    using System.Threading.Tasks;
    using Cars.Models;
    using Data.Models;

    public class PostsService : IPostsService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly CarDealershipDbContext data;

        public PostsService(CarDealershipDbContext data)
        {
            this.data = data;
        }

        public Task CreateAsync(CreatePostInputModel input, string userId, string imagePath)
        {
            var inputCar = input.Car;

            var car = new Car()
            {
                Make = inputCar.Make,
                Model = inputCar.Model,
                Description = inputCar.Description,
                CategoryId = inputCar.CategoryId,
                FuelTypeId = inputCar.FuelTypeId,
                TransmissionTypeId = inputCar.TransmissionTypeId,
                Year = inputCar.Year,
                Kilometers = inputCar.Kilometers,
                Horsepower = inputCar.Horsepower,
                Price = inputCar.Price
            };


            throw new NotImplementedException();
        }
    }
}