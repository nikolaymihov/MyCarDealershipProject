namespace MyCarDealership.Services.Cars
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Data;
    using Data.Models;
    using Images;
    using Models;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class CarsService : ICarsService
    {
        private readonly CarDealershipDbContext data;
        private readonly IImagesService imagesService;
        private readonly IConfigurationProvider mapperConfiguration;

        public CarsService(CarDealershipDbContext data, IImagesService imagesService, IMapper mapper)
        {
            this.data = data;
            this.imagesService = imagesService;
            this.mapperConfiguration = mapper.ConfigurationProvider;
        }
        
        public async Task<Car> GetCarFromInputModelAsync(CarFormInputModelDTO inputCar, List<int> selectedExtrasIds, string userId, string imagePath)
        {
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
                Price = inputCar.Price,
                LocationCountry = inputCar.LocationCountry,
                LocationCity = inputCar.LocationCity,
            };

            if (selectedExtrasIds.Any())
            {
                foreach (var extraId in selectedExtrasIds)
                {
                    var extra = this.data.Extras.FirstOrDefault(e => e.Id == extraId);

                    if (extra != null)
                    {
                        car.CarExtras.Add(new CarExtra
                        {
                            Extra = extra,
                            Car = car,
                        });
                    }
                }
            }

            if (!inputCar.Images.Any())
            {
                throw new Exception($"At least one car image is required.");
            }

            if (inputCar.Images.Count() > 10)
            {
                throw new Exception($"The maximum allowed number of photos is 10.");
            }
            
            foreach (var image in inputCar.Images)
            {
                var dbImage = await this.imagesService.UploadImageAsync(image, userId, imagePath);
                car.Images.Add(dbImage);
            }

            return car;
        }

        public async Task UpdateCarDataFromInputModelAsync(int carId, CarFormInputModelDTO inputCar, List<int> selectedExtrasIds, List<string> deletedImagesIds, string userId, string imagePath, string coverImageId)
        {
            var car = this.GetDbCarById(carId);

            if (car == null)
            {
                throw new Exception($"Unfortunately, such car in our system doesn't exist!");
            }
            
            car.Make = inputCar.Make;
            car.Model = inputCar.Model;
            car.Description = inputCar.Description;
            car.CategoryId = inputCar.CategoryId;
            car.FuelTypeId = inputCar.FuelTypeId;
            car.TransmissionTypeId = inputCar.TransmissionTypeId;
            car.Year = inputCar.Year;
            car.Kilometers = inputCar.Kilometers;
            car.Horsepower = inputCar.Horsepower;
            car.Price = inputCar.Price;
            car.LocationCountry = inputCar.LocationCountry;
            car.LocationCity = inputCar.LocationCity;

            if (selectedExtrasIds.Any())
            {
                var currentExtrasIds = this.data.CarExtras.Where(ce => ce.CarId == carId).Select(ce => ce.ExtraId).ToList();

                foreach (var extraId in selectedExtrasIds)
                {
                    var extra = this.data.Extras.FirstOrDefault(e => e.Id == extraId);

                    if (extra != null && !currentExtrasIds.Contains(extraId))
                    {
                        car.CarExtras.Add(new CarExtra
                        {
                            Extra = extra,
                            Car = car,
                        });
                    }
                }

                if (selectedExtrasIds.Count < currentExtrasIds.Count)
                {
                    var deletedExtrasIds = currentExtrasIds.Where(extraId => !selectedExtrasIds.Contains(extraId)).ToList();

                    foreach (var deletedExtraId in deletedExtrasIds)
                    {
                       var deletedCarExtra = this.data.CarExtras.First(ce => ce.CarId == carId && ce.ExtraId == deletedExtraId);

                       this.data.CarExtras.Remove(deletedCarExtra);
                    }
                }
            }

            var currentImages = this.data.Images.Where(img => img.CarId == carId).ToList();
            
            if (deletedImagesIds.Count >= currentImages.Count && !inputCar.Images.Any())
            {
                throw new Exception($"You cannot delete all car images. At least one car image is required for each post.");
            }

            if (deletedImagesIds.Any())
            {
                foreach (var deletedImageId in deletedImagesIds)
                {
                    if (currentImages.Any(img => img.Id == deletedImageId))
                    {
                        var imageToRemove = this.data.Images.First(img => img.Id == deletedImageId);
                        this.data.Images.Remove(imageToRemove);
                    }
                }
            }

            var currentCoverImage = currentImages.FirstOrDefault(img => img.IsCoverImage);

            if (currentCoverImage == null || currentCoverImage.Id != coverImageId)
            {
                await this.imagesService.SetCoverImagePropertyAsync(coverImageId);
            }

            if (currentCoverImage != null && currentCoverImage.Id != coverImageId)
            {
                await this.imagesService.RemoveCoverImagePropertyAsync(currentCoverImage.Id);
            }

            if (inputCar.Images != null)
            {
                if (inputCar.Images.Count() + currentImages.Count > 10)
                {
                    throw new Exception($"The maximum allowed number of car images is 10.");
                }
                
                foreach (var image in inputCar.Images)
                {
                    var dbImage = await this.imagesService.UploadImageAsync(image, userId, imagePath);
                    car.Images.Add(dbImage);
                }
            }

            await this.data.SaveChangesAsync();
        }

        public async Task DeleteCarByIdAsync(int carId)
        {
            var car = this.GetDbCarById(carId);

            car.IsDeleted = true;
            car.DeletedOn = DateTime.UtcNow;
            
            await this.data.SaveChangesAsync();
        }

        public IEnumerable<BaseCarSpecificationServiceModel> GetAllCategories()
        {
            return this.data
                .Categories
                .ProjectTo<BaseCarSpecificationServiceModel>(this.mapperConfiguration)
                .ToList();
        }

        public IEnumerable<BaseCarSpecificationServiceModel> GetAllFuelTypes()
        {
            return this.data
                .FuelTypes
                .ProjectTo<BaseCarSpecificationServiceModel>(this.mapperConfiguration)
                .ToList();
        }

        public IEnumerable<BaseCarSpecificationServiceModel> GetAllTransmissionTypes()
        {
            return this.data
                .TransmissionTypes
                .ProjectTo<BaseCarSpecificationServiceModel>(this.mapperConfiguration)
                .ToList();
        }

        public IEnumerable<CarExtrasServiceModel> GetAllCarExtras()
        {
            return this.data
                .Extras        
                .OrderBy(e => e.TypeId)
                .ProjectTo<CarExtrasServiceModel>(this.mapperConfiguration)
                .ToList();
        }

        public void FillInputCarBaseProperties(BaseCarInputModelDTO inputCar)
        {
            inputCar.Categories = this.GetAllCategories();
            inputCar.FuelTypes = this.GetAllFuelTypes();
            inputCar.TransmissionTypes = this.GetAllTransmissionTypes();
            inputCar.CarExtras = this.GetAllCarExtras();
        }

        private Car GetDbCarById(int carId)
        {
            return this.data.Cars.FirstOrDefault(c => c.Id == carId && !c.IsDeleted);
        }
    }
}