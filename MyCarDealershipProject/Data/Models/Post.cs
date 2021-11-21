namespace MyCarDealershipProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public int Id { get; init; }

        public int CarId { get; set; }

        public Car Car { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        public DateTime PublishedOn { get; init; }
    }
}