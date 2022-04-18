namespace MyCarDealership.MapperConfigurations.Profiles
{
    using AutoMapper;
    using Data.Models;
    using Services.Cars.Models;
    using Web.ViewModels.Cars;

    public class CarsProfile : Profile
    {
        public CarsProfile()
        {
            this.CreateMap<Category, BaseCarSpecificationServiceModel>();

            this.CreateMap<FuelType, BaseCarSpecificationServiceModel>();

            this.CreateMap<TransmissionType, BaseCarSpecificationServiceModel>();

            this.CreateMap<Extra, CarExtrasServiceModel>();

            this.CreateMap<BaseCarSpecificationServiceModel, BaseCarSpecificationViewModel>().ReverseMap();

            this.CreateMap<CarExtrasServiceModel, CarExtrasViewModel>().ReverseMap();

            this.CreateMap<BaseCarDTO, BaseCarViewModel>().ReverseMap();

            this.CreateMap<CarFormInputModelDTO, CarFormInputModel>().ReverseMap();

            this.CreateMap<SearchCarInputModelDTO, SearchCarInputModel>().ReverseMap();

            this.CreateMap<CarInListDTO, CarInListViewModel>().ReverseMap();

            this.CreateMap<SingleCarDTO, SingleCarViewModel>().ReverseMap();

            this.CreateMap<CarByUserDTO, CarByUserViewModel>().ReverseMap();

            this.CreateMap<LatestPostsCarDTO, LatestPostsCarViewModel>().ReverseMap();
        }
    }
}