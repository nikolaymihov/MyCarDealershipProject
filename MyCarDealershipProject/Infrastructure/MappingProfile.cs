namespace MyCarDealershipProject.Infrastructure
{
    using AutoMapper;
    using Data.Models;
    using Models.Cars;
    using Services.Cars.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Category, CarCategoryServiceModel>();

            this.CreateMap<FuelType, CarFuelTypeServiceModel>();
            
            this.CreateMap<TransmissionType, CarTransmissionTypeServiceModel>();

            this.CreateMap<Extra, CarExtrasServiceModel>();
        }
    }
}
