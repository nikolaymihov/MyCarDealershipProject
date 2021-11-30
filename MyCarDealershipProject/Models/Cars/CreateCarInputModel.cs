namespace MyCarDealershipProject.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using Services.Cars.Models;

    public class CreateCarInputModel : BaseCarInputModel
    {
        [Display(Name = "Car category:")]
        public int CategoryId { get; init; }

        public IEnumerable<CarCategoryServiceModel> Categories { get; set; }

        [Display(Name = "Car images:")]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
