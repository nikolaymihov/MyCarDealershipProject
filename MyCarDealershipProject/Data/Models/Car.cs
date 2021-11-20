namespace MyCarDealershipProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Car
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(CarMakeMaxLength)]
        public string Make  { get; set; }

        [Required]
        [MaxLength(CarModelMaxLength)]
        public string Model { get; set; }

        [Required]
        public string Description { get; set; }
        
        public int CategoryId { get; set; }

        public Category Category { get; init; }

        public int Year { get; set; }
    }
}