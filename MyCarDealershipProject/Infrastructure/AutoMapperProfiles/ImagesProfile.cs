namespace MyCarDealershipProject.Infrastructure.AutoMapperProfiles
{
    using AutoMapper;
    using Models.Images;
    using Services.Images.Models;

    public class ImagesProfile : Profile
    {
        public ImagesProfile()
        {
            this.CreateMap<ImageInfoDTO, ImageInfoViewModel>().ReverseMap();
        }
    }
}