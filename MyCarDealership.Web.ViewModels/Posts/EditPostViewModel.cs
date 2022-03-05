namespace MyCarDealership.Web.ViewModels.Posts
{
    using System.Collections.Generic;
    using Images;

    public class EditPostViewModel : PostFormInputModel
    {
        public IEnumerable<ImageInfoViewModel> CurrentImages { get; set; } = new HashSet<ImageInfoViewModel>();

        public string SelectedCoverImageId { get; set; }

        public IEnumerable<string> DeletedImagesIds { get; set; } = new HashSet<string>();

        public string CreatorId { get; set; }

        public int CarId { get; set; }
    }
}