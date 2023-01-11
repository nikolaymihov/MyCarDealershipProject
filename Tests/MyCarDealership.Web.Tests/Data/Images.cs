namespace MyCarDealership.Web.Tests.Data
{
    using System.IO;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;

    public static class Images
    {
        public static IEnumerable<IFormFile> GetTestImages()
        {
            var imagesList = new List<IFormFile>();
            var bytes = Encoding.UTF8.GetBytes("Test image file");
            var testImage = new FormFile
            (
                baseStream: new MemoryStream(bytes), 
                baseStreamOffset: 0, 
                length: bytes.Length, 
                name: "Data", 
                fileName: "testImage.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/csv"
            };

            imagesList.Add(testImage);

            return imagesList;
        }
    }
}