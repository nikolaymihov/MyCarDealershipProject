namespace MyCarDealership.MapperConfig.Profiles
{
    using AutoMapper;
    using Services.Images.Models;
    using Web.ViewModels.Images;

    public class ImagesProfile : Profile
    {
        public ImagesProfile()
        {
            this.CreateMap<ImageInfoDTO, ImageInfoViewModel>().ReverseMap();
        }
    }
}