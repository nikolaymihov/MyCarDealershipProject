namespace MyCarDealershipProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Post
    {
        public int Id { get; init; }

        public DateTime PublishedOn { get; init; }

        [Required]
        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }
        
        public int CarId { get; set; }

        public Car Car { get; set; }

        [Required]
        [MaxLength(PostSellerNameMaxLength)]
        public string SellerName { get; set; }

        [Required]
        [MaxLength(PostSellerPhoneNumberMaxLength)]
        public string SellerPhoneNumber { get; set; }

        [Required]
        [MaxLength(PostCarLocationCountryMaxLength)]
        public string CarLocationCountry { get; set; }

        [Required]
        [MaxLength(PostCarLocationCityMaxLength)]
        public string CarLocationCity { get; set; }
    }
}