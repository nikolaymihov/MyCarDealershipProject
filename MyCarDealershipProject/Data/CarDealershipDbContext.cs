namespace MyCarDealershipProject.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class CarDealershipDbContext : IdentityDbContext
    {
        public CarDealershipDbContext(DbContextOptions<CarDealershipDbContext> options)
            : base(options)
        {
        }
    }
}
