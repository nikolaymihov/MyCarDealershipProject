namespace MyCarDealershipProject.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class CarDealershipDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(CarDealershipDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            
            var seeders = new List<ISeeder>
            {
                new CategoriesSeeder(),
                new ExtraTypesSeeder(),
                new FuelTypesSeeder(),
                new TransmissionTypesSeeder(),
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
