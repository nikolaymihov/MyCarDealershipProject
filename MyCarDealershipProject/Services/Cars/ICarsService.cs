namespace MyCarDealershipProject.Services.Cars
{
    using System.Collections.Generic;
    using Models;

    public interface ICarsService
    {
        IEnumerable<CarCategoryServiceModel> GetAllCategories();

        IEnumerable<CarFuelTypeServiceModel> GetAllFuelTypes();

        IEnumerable<CarTransmissionTypeServiceModel> GetAllTransmissionTypes();

        IEnumerable<CarExtrasServiceModel> GetAllCarExtras();
    }
}