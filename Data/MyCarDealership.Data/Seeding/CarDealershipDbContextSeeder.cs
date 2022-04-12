namespace MyCarDealership.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CarDealershipDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(CarDealershipDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var seeders = new List<ISeeder>
            {
                new CategoriesSeeder(),
                new ExtraTypesSeeder(),
                new FuelTypesSeeder(),
                new TransmissionTypesSeeder(),
                new AdministratorSeeder()
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
