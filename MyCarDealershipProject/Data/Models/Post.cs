namespace MyCarDealershipProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public int Id { get; init; }

        public DateTime PublishedOn { get; init; }

        [Required]
        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }
        
        public int CarId { get; set; }

        public Car Car { get; set; }
    }
}