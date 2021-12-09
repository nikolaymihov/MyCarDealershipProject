namespace MyCarDealershipProject.Models.Posts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Cars;

    using static Data.DataConstants;

    public class CreatePostInputModel
    {
        [Required]
        public CreateCarInputModel Car { get; set; }

        public IEnumerable<int> SelectedExtrasIds { get; set; } = new HashSet<int>();

        public DateTime PublishedOn { get; init; }

        [Required(ErrorMessage = "The seller name field is required.")]
        [StringLength(
            PostSellerNameMaxLength,
            MinimumLength = PostSellerNameMinLength,
            ErrorMessage = "The seller name must be between {2} and {1} characters long.")]
        [Display(Name = "Seller name:")]
        public string SellerName { get; set; }

        [Required(ErrorMessage = "The seller phone number field is required.")]
        [StringLength(
            PostSellerPhoneNumberMaxLength,
            MinimumLength = PostSellerPhoneNumberMinLength,
            ErrorMessage = "The seller phone number must be between {2} and {1} digits long.")]
        [Display(Name = "Seller phone number:")]
        public string SellerPhoneNumber { get; set; }
    }
}
