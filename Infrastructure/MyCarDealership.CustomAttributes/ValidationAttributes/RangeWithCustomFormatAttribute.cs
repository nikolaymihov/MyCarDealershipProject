namespace MyCarDealership.CustomAttributes.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;

    public class RangeWithCustomFormatAttribute : RangeAttribute
    {
        public RangeWithCustomFormatAttribute(int minimum, int maximum, string propertyDisplayName) : base(minimum,
            maximum)
        {
            this.ErrorMessage = $"The {propertyDisplayName} must be between {minimum:N0} and {maximum:N0}."; //1000000 ==> 1 000 000
        }
    }
}