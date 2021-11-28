namespace MyCarDealershipProject.Models.Posts
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    using Cars;

    public class CreatePostInputModel
    {
        [Required]
        public CreateCarInputModel Car { get; set; }
        
        public DateTime PublishedOn { get; init; }
    }
}
