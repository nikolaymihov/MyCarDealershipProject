namespace MyCarDealershipProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Car
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(CarMakeMaxLength)]
        public string Make { get; set; }

        [Required]
        [MaxLength(CarModelMaxLength)]
        public string Model { get; set; }

        [Required]
        public string Description { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }

        public int Kilometers { get; set; }

        public int Horsepower { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int FuelTypeId { get; set; }

        public FuelType FuelType { get; set; }

        public int TransmissionTypeId { get; set; }

        public TransmissionType TransmissionType { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public ICollection<CarExtra> CarExtras { get; set; } = new HashSet<CarExtra>();

        public ICollection<Image> Images { get; set; } = new HashSet<Image>();
    }
}