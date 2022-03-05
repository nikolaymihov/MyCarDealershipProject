namespace MyCarDealership.Services.Posts.Models
{
    using System.Collections.Generic;
    using Cars.Models;

    public class PostFormInputModelDTO
    {
        public CarFormInputModelDTO Car { get; set; }

        public IEnumerable<int> SelectedExtrasIds { get; set; } = new HashSet<int>();
        
        public string SellerName { get; set; }
        
        public string SellerPhoneNumber { get; set; }
    }
}
