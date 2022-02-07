namespace MyCarDealershipProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Post
    {
        public int Id { get; init; }

        public DateTime PublishedOn { get; init; }

        public DateTime? ModifiedOn { get; set; }

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
    }
}