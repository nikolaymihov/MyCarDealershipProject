namespace MyCarDealership.Data.Seeding
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;

    public class TransmissionTypesSeeder : ISeeder
    {
        public async Task SeedAsync(CarDealershipDbContext dbContext)
        {
            if (dbContext.TransmissionTypes.Any())
            {
                return;
            }
            
            var transmissionTypesToSeed = new List<TransmissionType>()
            {
                new TransmissionType() { Name = "Manual gearbox"},
                new TransmissionType() { Name = "Automatic transmission"},
                new TransmissionType() { Name = "Semi-automatic"},
            };

            await dbContext.TransmissionTypes.AddRangeAsync(transmissionTypesToSeed);
            await dbContext.SaveChangesAsync();
        }
    }
}