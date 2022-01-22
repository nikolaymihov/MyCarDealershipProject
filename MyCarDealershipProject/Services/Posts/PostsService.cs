namespace MyCarDealershipProject.Services.Posts
{
    using System;
    using System.Linq;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Data;
    using Data.Models;
    using Models;
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

        public IEnumerable<PostInListViewModel> GetPostsByPage(IEnumerable<PostInListViewModel> posts, int page, int postsPerPage = 12)
        {
            return posts.Skip((page - 1) * postsPerPage).Take(postsPerPage).ToList();
        }

        public IEnumerable<PostInListViewModel> GetMatchingPosts(SearchPostInputModel searchInputModel, PostsSorting sorting = PostsSorting.NewestFirst)
        {
            var postsQuery = this.data.Posts.AsQueryable();

            if (searchInputModel.Car != null)
            {
                var searchedCarDetails = searchInputModel.Car;

                if (!string.IsNullOrWhiteSpace(searchedCarDetails.TextSearchTerm))
                {
                    postsQuery = postsQuery.Where(p =>
                        (p.Car.Make + " " + p.Car.Model + " " + p.Car.Description).ToLower()
                        .Contains(searchedCarDetails.TextSearchTerm.ToLower()));
                }

                if (searchedCarDetails.FromYear is > 0)
                {
                    postsQuery = postsQuery.Where(p =>
                        p.Car.Year >= searchedCarDetails.FromYear);
                }

                if (searchedCarDetails.ToYear is > 0)
                {
                    postsQuery = postsQuery.Where(p =>
                        p.Car.Year <= searchedCarDetails.ToYear);
                }

                if (searchedCarDetails.MinHorsepower is > 0)
                {
                    postsQuery = postsQuery.Where(p =>
                        p.Car.Horsepower >= searchedCarDetails.MinHorsepower);
                }

                if (searchedCarDetails.MaxHorsepower is > 0)
                {
                    postsQuery = postsQuery.Where(p =>
                        p.Car.Horsepower <= searchedCarDetails.MaxHorsepower);
                }

                if (searchedCarDetails.MinPrice is > 0)
                {
                    postsQuery = postsQuery.Where(p =>
                        p.Car.Price >= searchedCarDetails.MinPrice);
                }

                if (searchedCarDetails.MaxPrice is > 0)
                {
                    postsQuery = postsQuery.Where(p =>
                        p.Car.Price <= searchedCarDetails.MaxPrice);
                }
            }

            if (searchInputModel.SelectedCategoriesIds.Any())
            {
                postsQuery = postsQuery.Where(p => searchInputModel.SelectedCategoriesIds.Contains(p.Car.CategoryId));
            }

            if (searchInputModel.SelectedFuelTypesIds.Any())
            {
                postsQuery = postsQuery.Where(p => searchInputModel.SelectedFuelTypesIds.Contains(p.Car.FuelTypeId));
            }

            if (searchInputModel.SelectedTransmissionTypesIds.Any())
            {
                postsQuery = postsQuery.Where(p => searchInputModel.SelectedTransmissionTypesIds.Contains(p.Car.TransmissionTypeId));
            }

            if (searchInputModel.SelectedExtrasIds.Any())
            {
                var searchedExtrasIds = searchInputModel.SelectedExtrasIds;
                var currentQueuedCars = postsQuery.Select(p => p.Car).ToList();
                var allMatchedCarIds = new List<int>();

                
                foreach (var car in currentQueuedCars)
                {
                    var currentCarExtrasIds = data.CarExtras
                                                            .Where(ce => ce.Car.Id == car.Id)
                                                            .Select(ce => ce.ExtraId)
                                                            .ToList();

                    //The below code checks if all the searched extras are contained in the current car extras
                    if (searchedExtrasIds.Intersect(currentCarExtrasIds).Count() == searchedExtrasIds.Count())
                    {
                        allMatchedCarIds.Add(car.Id);
                    }
                }

                postsQuery = postsQuery.Where(p => allMatchedCarIds.Contains(p.Car.Id));
            }

            if (!postsQuery.Any())
            {
                throw new Exception("Unfortunately, there are no cars in our system that match your search criteria.");
            }
            
            postsQuery = sorting switch
            {
                PostsSorting.OldestFirst => postsQuery.OrderBy(p => p.Id),
                PostsSorting.PriceHighestFirst => postsQuery.OrderByDescending(p => p.Car.Price),
                PostsSorting.PriceLowestFirst => postsQuery.OrderBy(p => p.Car.Price),
                PostsSorting.HorsepowerHighestFirst => postsQuery.OrderByDescending(p => p.Car.Horsepower),
                PostsSorting.HorsepowerLowestFirst => postsQuery.OrderBy(p => p.Car.Horsepower),
                PostsSorting.CarYearNewestFirst => postsQuery.OrderByDescending(p => p.Car.Year),
                PostsSorting.CarYearOldestFirst => postsQuery.OrderBy(p => p.Car.Year),
                PostsSorting.NewestFirst or _ => postsQuery.OrderByDescending(p => p.Id)
            };

            var posts = postsQuery
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
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                }).ToList();

            return posts;
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
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                    SellerName = p.SellerName,
                    SellerPhoneNumber = p.SellerPhoneNumber
                })
                .FirstOrDefault();

            return post;
        }

        public IEnumerable<PostInLatestListViewModel> GetLatest(int count)
        {
            var posts = this.data.Posts
                .OrderByDescending(p => p.PublishedOn)
                .Take(count)
                .Select(p => new PostInLatestListViewModel()
                {
                    Car = new LatestPostsCarViewModel()
                    {
                        Id = p.Car.Id,
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        Price = p.Car.Price,
                        Year = p.Car.Year,
                        Horsepower = p.Car.Horsepower,
                        FuelType = p.Car.FuelType.Name,
                        TransmissionType = p.Car.TransmissionType.Name,
                        CoverImage = "/images/cars/" + p.Car.Images.FirstOrDefault().Id + "." +
                                     p.Car.Images.FirstOrDefault().Extension,
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                }).ToList();

            return posts;
        }

        private static string GetFormattedDate(DateTime inputDateTime)
        {
            if (inputDateTime.Date == DateTime.UtcNow.Date)
            {
                return "Today, " + inputDateTime.ToString("t", CultureInfo.InvariantCulture);
            }

            return inputDateTime.ToString("d", CultureInfo.InvariantCulture);
        }
    }
}