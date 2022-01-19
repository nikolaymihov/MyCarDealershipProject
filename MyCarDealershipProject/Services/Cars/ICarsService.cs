namespace MyCarDealershipProject.Services.Cars
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;
    using Data.Models;
    using MyCarDealershipProject.Models.Cars;

    public interface ICarsService
    {
        Task<Car> GetCarFromInputModel(CreateCarInputModel inputCar, List<int> selectedExtrasIds, string userId, string imagePath);

        IEnumerable<CarCategoryServiceModel> GetAllCategories();

        IEnumerable<CarFuelTypeServiceModel> GetAllFuelTypes();

        IEnumerable<CarTransmissionTypeServiceModel> GetAllTransmissionTypes();

        IEnumerable<CarExtrasServiceModel> GetAllCarExtras();

        void FillBaseInputCarProperties(BaseCarInputModel inputCar);
    }
}