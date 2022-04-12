namespace MyCarDealership.Services.Cars
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;
    using Data.Models;

    public interface ICarsService
    {
        Task<Car> GetCarFromInputModelAsync(CarFormInputModelDTO inputCar, List<int> selectedExtrasIds, string userId, string imagePath);

        IEnumerable<BaseCarSpecificationServiceModel> GetAllCategories();

        IEnumerable<BaseCarSpecificationServiceModel> GetAllFuelTypes();

        IEnumerable<BaseCarSpecificationServiceModel> GetAllTransmissionTypes();

        IEnumerable<CarExtrasServiceModel> GetAllCarExtras();

        void FillInputCarBaseProperties(BaseCarInputModelDTO inputCar);

        Task UpdateCarDataFromInputModelAsync(int carId, CarFormInputModelDTO inputCar, List<int> selectedExtrasIds, List<string> deletedImagesIds, string userId, string imagePath, string coverImageId);

        Task DeleteCarByIdAsync(int carId);
    }
}