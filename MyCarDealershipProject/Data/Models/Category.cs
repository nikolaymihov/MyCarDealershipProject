namespace MyCarDealershipProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Category
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(CategoryMaxLength)]
        public string Name { get; set; }

        public ICollection<Car> Cars { get; init; } = new HashSet<Car>();
    }
}