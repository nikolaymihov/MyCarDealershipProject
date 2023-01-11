namespace MyCarDealership.Services.Posts
{
    using System;
    using System.Linq;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Cars;
    using Cars.Models;
    using Data;
    using Data.Models;
    using Images;
    using Images.Models;
    using Models;

    public class PostsService : IPostsService
    {
        private readonly CarDealershipDbContext data;
        private readonly ICarsService carsService;
        private readonly IImagesService imagesService;

        public PostsService(CarDealershipDbContext data, ICarsService carsService, IImagesService imagesService)
        {
            this.data = data;
            this.carsService = carsService;
            this.imagesService = imagesService;
        }

        public async Task<int> CreateAsync(PostFormInputModelDTO inputPost, Car car, string userId, bool isPublic)
        {
            var post = new Post
            {
                Car = car,
                CreatorId = userId,
                PublishedOn = DateTime.UtcNow,
                SellerName = inputPost.SellerName,
                SellerPhoneNumber = inputPost.SellerPhoneNumber,
                IsPublic = isPublic,
            };

            await this.data.Posts.AddAsync(post);
            await this.data.SaveChangesAsync();

            return post.Id;
        }

        public IEnumerable<T> GetPostsByPage<T>(IEnumerable<T> posts, int page, int postsPerPage)
        {
            return posts.Skip((page - 1) * postsPerPage).Take(postsPerPage).ToList();
        }

        public IEnumerable<PostByUserDTO> GetPostsByUser(string userId, int sortingNumber)
        {
            var postsQuery = this.data.Posts
                .Where(p => p.CreatorId == userId && !p.IsDeleted).AsQueryable();
            
            postsQuery = GetSortedPosts(postsQuery, sortingNumber);

            var posts = postsQuery    
                .Select(p => new PostByUserDTO()
                {
                    Car = new CarByUserDTO()
                    {
                        Id = p.Car.Id,
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        Price = p.Car.Price,
                        Year = p.Car.Year,
                        CoverImage = this.imagesService.GetCoverImagePath(p.Car.Images.ToList()),
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                }).ToList();

            return posts;
        }

        public IEnumerable<PostInListDTO> GetMatchingPosts(SearchPostDTO searchInputModel, int sortingNumber)
        {
            var postsQuery = this.data.Posts.Where(p => !p.IsDeleted && p.IsPublic).AsQueryable();
            
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
                throw new Exception("Unfortunately, there are no cars in our system that match this search criteria.");
            }

            postsQuery = GetSortedPosts(postsQuery, sortingNumber);

            var posts = postsQuery
                .Select(p => new PostInListDTO()
                {
                    Car = new CarInListDTO()
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
                        CoverImage = this.imagesService.GetCoverImagePath(p.Car.Images.ToList()),
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                }).ToList();

            return posts;
        }

        public int GetAllPostsCount()
        {
            return this.data.Posts.Count();
        }

        public IEnumerable<BasePostInListDTO> GetAllPostsBaseInfo(int page, int postsPerPage)
        {
            var posts = this.data.Posts
                .Where(p => !p.IsDeleted)
                .OrderBy(p => p.IsPublic)
                .ThenByDescending(p => p.PublishedOn)
                .Skip((page - 1) * postsPerPage).Take(postsPerPage)
                .Select(p => new BasePostInListDTO()
                {
                    Car = new BaseCarDTO()
                    {
                        Id = p.Car.Id,
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        Year = p.Car.Year,
                        Price = p.Car.Price,
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                    IsPublic = p.IsPublic
                }).ToList();

            return posts;
        }

        public SinglePostDTO GetSinglePostViewModelById(int postId, bool publicOnly = true)
        {
            var post = this.data.Posts
                .Where(p => p.Id == postId && !p.IsDeleted && (!publicOnly || p.IsPublic))
                .Select(p => new SinglePostDTO()
                {
                    Car = new SingleCarDTO()
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
                        Images = p.Car.Images.OrderByDescending(img => img.IsCoverImage)
                                             .Select(img => this.imagesService.GetDefaultCarImagesPath(img.Id, img.Extension))
                                             .ToList(),
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                    SellerName = p.SellerName,
                    SellerPhoneNumber = p.SellerPhoneNumber
                })
                .FirstOrDefault();

            return post;
        }

        public EditPostDTO GetPostFormInputModelById(int postId)
        {
            var post = this.data.Posts
                .Where(p => p.Id == postId && !p.IsDeleted)
                .Select(p => new EditPostDTO()
                {
                    Car = new CarFormInputModelDTO()
                    {
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        Description = p.Car.Description,
                        Price = p.Car.Price,
                        Year = p.Car.Year,
                        Kilometers = p.Car.Kilometers,
                        Horsepower = p.Car.Horsepower,
                        CategoryId = p.Car.CategoryId,
                        FuelTypeId = p.Car.FuelTypeId,
                        TransmissionTypeId = p.Car.TransmissionTypeId,
                        LocationCity = p.Car.LocationCity,
                        LocationCountry = p.Car.LocationCountry,
                    },
                    SelectedExtrasIds = p.Car.CarExtras.Select(ce => ce.ExtraId).ToList(),
                    SellerName = p.SellerName,
                    SellerPhoneNumber = p.SellerPhoneNumber,
                    CreatorId = p.CreatorId,
                    CurrentImages = p.Car.Images.OrderByDescending(img => img.IsCoverImage)
                                                .Select(img => new ImageInfoDTO()
                                                        {
                                                            Id = img.Id,
                                                            Path = this.imagesService.GetDefaultCarImagesPath(img.Id, img.Extension),
                                                        }).ToList(),
                    SelectedCoverImageId = p.Car.Images.FirstOrDefault(img => img.IsCoverImage).Id,
                    CarId = p.CarId,
                }).FirstOrDefault();

            return post;
        }

        public IEnumerable<ImageInfoDTO> GetCurrentDbImagesForAPost(int postId)
        {
             var post = this.data.Posts.FirstOrDefault(p => p.Id == postId && !p.IsDeleted);
             var car = this.data.Cars.FirstOrDefault(c => c.Id == post.CarId && !c.IsDeleted);
             var postImages = this.data.Images
                                        .Where(img => img.CarId == car.Id)
                                        .OrderByDescending(img => img.IsCoverImage)
                                        .Select(img => new ImageInfoDTO()
                                        {
                                            Id = img.Id, 
                                            Path = this.imagesService.GetDefaultCarImagesPath(img.Id, img.Extension),
                                        }).ToList();

             return postImages;
        }

        public IEnumerable<PostInLatestListDTO> GetLatest(int count)
        {
            var posts = this.data.Posts
                .Where(p => !p.IsDeleted && p.IsPublic)
                .OrderByDescending(p => p.PublishedOn)
                .Take(count)
                .Select(p => new PostInLatestListDTO()
                {
                    Car = new LatestPostsCarDTO()
                    {
                        Id = p.Car.Id,
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        Price = p.Car.Price,
                        Year = p.Car.Year,
                        Horsepower = p.Car.Horsepower,
                        FuelType = p.Car.FuelType.Name,
                        TransmissionType = p.Car.TransmissionType.Name,
                        CoverImage = this.imagesService.GetCoverImagePath(p.Car.Images.ToList())
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                }).ToList();

            return posts;
        }

        public async Task UpdateAsync(int postId, EditPostDTO editedPost, bool isPublic)
        {
            var post = this.GetDbPostById(postId);

            if (post == null)
            {
                throw new Exception($"Unfortunately, we cannot find such post in our system!");
            }
            
            post.ModifiedOn = DateTime.UtcNow;
            post.SellerName = editedPost.SellerName;
            post.SellerPhoneNumber = editedPost.SellerPhoneNumber;
            post.IsPublic = isPublic;
            
            await this.data.SaveChangesAsync();
        }

        public async Task ChangeVisibilityAsync(int postId)
        {
            var post = this.GetDbPostById(postId);

            post.IsPublic = !post.IsPublic;

            await this.data.SaveChangesAsync();
        }

        public PostByUserDTO GetBasicPostInformationById(int postId)
        {
            var post = this.data.Posts
                .Where(p => p.Id == postId && !p.IsDeleted)
                .Select(p => new PostByUserDTO()
                {
                    Car = new CarByUserDTO()
                    {
                        Id = p.Car.Id,
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        Price = p.Car.Price,
                        Year = p.Car.Year,
                        CoverImage = this.imagesService.GetCoverImagePath(p.Car.Images.ToList()),
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                }).FirstOrDefault();

            return post;
        }

        public string GetPostCreatorId(int postId)
        {
            var post = this.GetDbPostById(postId);

            return post?.CreatorId;
        }

        public async Task DeletePostByIdAsync(int postId)
        {
            var post = this.GetDbPostById(postId);

            if (post == null)
            {
                throw new Exception($"Unfortunately, we cannot find such post in our system!");
            }

            await this.carsService.DeleteCarByIdAsync(post.Id);

            post.IsDeleted = true;
            post.DeletedOn = DateTime.UtcNow;
            post.IsPublic = false;
            
            await this.data.SaveChangesAsync();
        }

        private Post GetDbPostById(int postId)
        {
            return this.data.Posts.FirstOrDefault(p => p.Id == postId && !p.IsDeleted);
        }

        private static string GetFormattedDate(DateTime inputDateTime)
        {
            if (inputDateTime.Date == DateTime.UtcNow.Date)
            {
                return "Today, " + inputDateTime.ToString("t", CultureInfo.InvariantCulture);
            }

            return inputDateTime.ToString("d", CultureInfo.InvariantCulture);
        }
        
        private static IQueryable<Post> GetSortedPosts(IQueryable<Post> postsQuery, int sortingNumber)
        {
            postsQuery = sortingNumber switch
            {
                1 => postsQuery.OrderBy(p => p.Id),
                2 => postsQuery.OrderByDescending(p => p.Car.Price),
                3 => postsQuery.OrderBy(p => p.Car.Price),
                4 => postsQuery.OrderByDescending(p => p.Car.Horsepower),
                5 => postsQuery.OrderBy(p => p.Car.Horsepower),
                6 => postsQuery.OrderByDescending(p => p.Car.Year),
                7 => postsQuery.OrderBy(p => p.Car.Year),
                _ => postsQuery.OrderByDescending(p => p.Id),
            };

            return postsQuery;
        }
    }
}