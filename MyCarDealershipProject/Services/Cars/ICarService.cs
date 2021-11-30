namespace MyCarDealershipProject.Services.Cars
{
    using System.Collections.Generic;
    using Models;

    public interface ICarService
    {
        IEnumerable<CarCategoryServiceModel> GetAllCategories();
    }
}