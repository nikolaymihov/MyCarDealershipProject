namespace MyCarDealership.Web.ViewModels.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Cars;

    public class SearchPostInputModel
    {
        [Required]
        public SearchCarInputModel Car { get; set; }

        public IEnumerable<int> SelectedCategoriesIds { get; set; } = new HashSet<int>();

        public IEnumerable<int> SelectedFuelTypesIds { get; set; } = new HashSet<int>();

        public IEnumerable<int> SelectedTransmissionTypesIds { get; set; } = new HashSet<int>();

        public IEnumerable<int> SelectedExtrasIds { get; set; } = new HashSet<int>();
    }
}