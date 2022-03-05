namespace MyCarDealership.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class Extra
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(ExtraNameMaxLength)]
        public string Name { get; set; }

        public int TypeId { get; set; }

        public ExtraType Type { get; set; }

        public ICollection<CarExtra> CarExtras { get; set; } = new HashSet<CarExtra>();
    }
}