namespace MyCarDealership.Services.Cars.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;

    public class CarFormInputModelDTO : BaseCarInputModelDTO
    {
        public string Make { get; set; }
        
        public string Model { get; set; }
        
        public string Description { get; set; }
        
        public int Year { get; set; }
        
        public decimal Price { get; set; }
        
        public int Kilometers { get; set; }
        
        public int Horsepower { get; set; }
        
        public string LocationCountry { get; set; }
        
        public string LocationCity { get; set; }
        
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
