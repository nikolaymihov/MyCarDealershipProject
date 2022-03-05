namespace MyCarDealership.Services.Images
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Data.Models;

    public interface IImagesService
    {
        Task<Image> UploadImageAsync(IFormFile image, string userId, string imagePath);

        Task SetCoverImagePropertyAsync(string imageId);

        Task RemoveCoverImagePropertyAsync(string imageId);

        string GetDefaultCarImagesPath(string imageId, string imageExtension);

        string GetCoverImagePath(ICollection<Image> carImages);
    }
}