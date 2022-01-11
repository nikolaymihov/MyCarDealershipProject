namespace MyCarDealershipProject.Services.Cars
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Data;
    using Models;
    using AutoMapper;
    using Data.Models;
    using AutoMapper.QueryableExtensions;
    using MyCarDealershipProject.Models.Cars;

    public class CarsService : ICarsService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly CarDealershipDbContext data;
        private readonly IConfigurationProvider mapper;

        public CarsService(CarDealershipDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }
        
        public async Task<Car> GetCarFromInputModel(CreateCarInputModel inputCar, List<int> selectedExtrasIds, string userId, string imagePath)
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

            if (inputCar.Images.Count() > 10)
            {
                throw new Exception($"The maximum allowed number of photos is 10.");
            }

            // /wwwroot/images/cars/ce8f1a9e-6c4a-44a1-adf3-f7c3d54ff859.jpg
            Directory.CreateDirectory($"{imagePath}/cars/");

            foreach (var image in inputCar.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');

                if (!this.allowedExtensions.Any(ex => extension.EndsWith(ex)))
                {
                    throw new Exception($"Invalid image extension! The allowed extensions are {string.Join(", ", this.allowedExtensions)}.");
                }

                if (image.Length > (5 * 1024 * 1024))
                {
                    throw new Exception($"Invalid file size. The maximum allowed file size is 5Mb.");
                }

                var dbImage = new Image
                {
                    CreatorId = userId,
                    Extension = extension,
                };

                car.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/cars/{dbImage.Id}.{extension}";
                await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }
            
            return car;
        }

        public IEnumerable<CarCategoryServiceModel> GetAllCategories()
        {
            return this.data
                .Categories
                .ProjectTo<CarCategoryServiceModel>(this.mapper)
                .ToList();
        }

        public IEnumerable<CarFuelTypeServiceModel> GetAllFuelTypes()
        {
            return this.data
                .FuelTypes
                .ProjectTo<CarFuelTypeServiceModel>(this.mapper)
                .ToList();
        }

        public IEnumerable<CarTransmissionTypeServiceModel> GetAllTransmissionTypes()
        {
            return this.data
                .TransmissionTypes
                .ProjectTo<CarTransmissionTypeServiceModel>(this.mapper)
                .ToList();
        }

        public IEnumerable<CarExtrasServiceModel> GetAllCarExtras()
        {
            return this.data
                .Extras        
                .OrderBy(e => e.TypeId)
                .ProjectTo<CarExtrasServiceModel>(this.mapper)
                .ToList();
        }

        public void FillInputCarProperties(CreateCarInputModel inputCar)
        {
            inputCar.Categories = this.GetAllCategories();
            inputCar.FuelTypes = this.GetAllFuelTypes();
            inputCar.TransmissionTypes = this.GetAllTransmissionTypes();
            inputCar.CarExtras = this.GetAllCarExtras();
        }

        public void FillSearchCarProperties(SearchCarInputModel searchCar)
        {
            searchCar.Categories = this.GetAllCategories();
            searchCar.FuelTypes = this.GetAllFuelTypes();
            searchCar.TransmissionTypes = this.GetAllTransmissionTypes();
            searchCar.CarExtras = this.GetAllCarExtras();
        }
    }
}