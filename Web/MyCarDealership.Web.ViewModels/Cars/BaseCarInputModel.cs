namespace MyCarDealership.Web.ViewModels.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BaseCarInputModel
    {
        [Display(Name = "Category:")]
        public int CategoryId { get; init; }

        public IEnumerable<BaseCarSpecificationViewModel> Categories { get; set; }

        [Display(Name = "Fuel type:")]
        public int FuelTypeId { get; init; }

        public IEnumerable<BaseCarSpecificationViewModel> FuelTypes { get; set; }

        [Display(Name = "Transmission type:")]
        public int TransmissionTypeId { get; init; }

        public IEnumerable<BaseCarSpecificationViewModel> TransmissionTypes { get; set; }

        [Display(Name = "Extras:")]
        public int CarExtraId { get; init; }

        public IEnumerable<CarExtrasViewModel> CarExtras { get; set; }
    }
}
