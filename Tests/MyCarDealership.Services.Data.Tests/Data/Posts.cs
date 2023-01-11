namespace MyCarDealership.Services.Data.Tests.Data
{
    using System;
    using MyCarDealership.Data.Models;
    using MyCarDealership.Services.Posts.Models;
    
    using static Cars;
    using static TestDataConstants;

    public class Posts
    {
        public static Post ValidTestPublicPost => new()
        {
            Id = TestIdNumber,
            Car = ValidTestCar,
            CreatorId = TestUserId,
            IsPublic = true,
            SellerName = TestPostSellerName,
            SellerPhoneNumber = TestPostSellerPhoneNumber,
        };

        public static PostFormInputModelDTO ValidTestPostFormInputModelDTO => new()
        {
            SellerName = TestPostSellerName,
            SellerPhoneNumber = TestPostSellerPhoneNumber,
        };

        public static EditPostDTO ValidEditPostDTO => new()
        {
            Car = ValidUpdatedCatTestModel,
            SellerName = UpdatedTestPostSellerName,
            SellerPhoneNumber = UpdatedTestPostSellerPhoneNumber,
        };

        public static SearchPostDTO ValidSearchPostDTO => new()
        {
            Car = ValidSearchCarInputModelDTO,
        };
    }
}