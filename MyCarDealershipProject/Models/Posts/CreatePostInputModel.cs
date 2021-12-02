namespace MyCarDealershipProject.Models.Posts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Cars;

    public class CreatePostInputModel
    {
        [Required]
        public CreateCarInputModel Car { get; set; }

        public IEnumerable<int> SelectedExtrasIds { get; set; } = new HashSet<int>();

        public DateTime PublishedOn { get; init; }
    }
}
