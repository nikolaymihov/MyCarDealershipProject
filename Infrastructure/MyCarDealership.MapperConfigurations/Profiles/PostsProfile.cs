namespace MyCarDealership.MapperConfigurations.Profiles
{
    using AutoMapper;
    using Services.Posts.Models;
    using Web.ViewModels.Posts;

    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            this.CreateMap<PostFormInputModelDTO, PostFormInputModel>().ReverseMap();

            this.CreateMap<PostInListDTO, PostInListViewModel>().ReverseMap();

            this.CreateMap<SearchPostDTO, SearchPostInputModel>().ReverseMap();

            this.CreateMap<SinglePostDTO, SinglePostViewModel>().ReverseMap();

            this.CreateMap<PostByUserDTO, PostByUserViewModel>().ReverseMap();

            this.CreateMap<PostsByUserDTO, PostsByUserViewModel>().ReverseMap();

            this.CreateMap<PostInLatestListDTO, PostInLatestListViewModel>().ReverseMap();

            this.CreateMap<EditPostDTO, EditPostViewModel>().ReverseMap();

            this.CreateMap<BasePostInListDTO, PostInAdminAreaViewModel>().ReverseMap();

            this.CreateMap<BasePostsListDTO, PostsListAdminAreaViewModel>().ReverseMap();
        }
    }
}