namespace MyCarDealershipProject.Services.Cars
{
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models;

    public class CarService : ICarService
    {
        private readonly CarDealershipDbContext data;
        private readonly IConfigurationProvider mapper;

        public CarService(CarDealershipDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public IEnumerable<CarCategoryServiceModel> GetAllCategories()
        {
            return this.data
                .Categories
                .ProjectTo<CarCategoryServiceModel>(this.mapper)
                .ToList();
        }
    }
}