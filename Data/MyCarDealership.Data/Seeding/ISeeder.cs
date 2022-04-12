namespace MyCarDealership.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(CarDealershipDbContext dbContext, IServiceProvider serviceProvider);
    }
}
