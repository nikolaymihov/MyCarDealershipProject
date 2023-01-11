namespace MyCarDealership.Web.Tests.Data
{
    using System.Linq;
    using System.Collections.Generic;
    using ViewModels.Posts;
    using MyCarDealership.Data.Models;
    using MyCarDealership.Services.Posts.Models;

    using static Cars;
    using static TestDataConstants;

    public static class Posts
    {
        public static IEnumerable<Post> TenPublicPosts()
        {
            return Enumerable.Range(0, 10)
                             .Select(i => new Post
                             {
                                 IsPublic = true
                             });
        }

        public static PostFormInputModel ValidTestPostFormInputModel => new()
        {
            Car = ValidTestCarFormInputModel,
            SellerName = TestPostSellerName,
            SellerPhoneNumber = TestPostSellerPhoneNumber,
        };

        public static IEnumerable<PostInListDTO> TenPublicPostInListDTOs()
        {
            return Enumerable.Range(0, 10)
                             .Select(i => new PostInListDTO
                             {
                                 Car = ValidCarInListDTO,
                                 PublishedOn = TestPostPublicationYear,
                             });
        }

        public static SinglePostDTO ValidSinglePostDTO(int id) => new()
        {
            Car = ValidSingleCarDTO(id),
            SellerName = TestPostSellerName,
            SellerPhoneNumber = TestPostSellerPhoneNumber,
            PublishedOn = TestPostPublicationYear,
        };

        public static IEnumerable<PostByUserDTO> TenPublicPostByUserDTOs()
        {
            return Enumerable.Range(0, 10)
                             .Select(i => new PostByUserDTO
                             {
                                 Car = ValidCarByUserDTO,
                                 PublishedOn = TestPostPublicationYear,
                             });
        }

        public static EditPostDTO ValidTestEditPostDTO => new()
        {
            Car = ValidTestCarFormInputModelDTO,
            SellerName = TestPostSellerName,
            SellerPhoneNumber = TestPostSellerPhoneNumber,
        };

        public static PostByUserDTO ValidTestPostByUserDTO => new()
        {
            Car = ValidCarByUserDTO,
            PublishedOn = TestPostPublicationYear,
        };
    }
}