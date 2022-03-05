namespace MyCarDealership.Services.Posts.Models
{
    using System.Collections.Generic;
    using Images.Models;

    public class EditPostDTO : PostFormInputModelDTO
    {
        public IEnumerable<ImageInfoDTO> CurrentImages { get; set; } = new HashSet<ImageInfoDTO>();

        public string SelectedCoverImageId { get; set; }

        public IEnumerable<string> DeletedImagesIds { get; set; } = new HashSet<string>();

        public string CreatorId { get; set; }

        public int CarId { get; set; }
    }
}