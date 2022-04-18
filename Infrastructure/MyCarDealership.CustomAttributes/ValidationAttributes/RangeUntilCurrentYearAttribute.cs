namespace MyCarDealership.CustomAttributes.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RangeUntilCurrentYearAttribute : RangeAttribute
    {
        public RangeUntilCurrentYearAttribute(int minYear) : base(minYear, DateTime.UtcNow.Year)
        {
        }
    }
}