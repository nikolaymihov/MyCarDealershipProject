namespace MyCarDealership.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class ExtraType
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(ExtraTypeNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Extra> Extras { get; set; } = new HashSet<Extra>();
    }
}