namespace MyCarDealershipProject.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class CreateCarInputModel : BaseCarInputModel
    {
        [Display(Name = "Car images:")]
        public IEnumerable<IFormFile> Images { get; set; }

    }
}
