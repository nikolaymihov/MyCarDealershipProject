namespace MyCarDealership.Data.Models
{
    public class CarExtra
    {
        public int CarId { get; set; }

        public Car Car { get; set; }

        public int ExtraId { get; set; }

        public Extra Extra { get; set; }
    }
}