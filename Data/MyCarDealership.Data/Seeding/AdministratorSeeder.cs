namespace MyCarDealership.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Models;

    using static GlobalConstants.GlobalConstants;

    public class AdministratorSeeder : ISeeder
    {
        public async Task SeedAsync(CarDealershipDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedAdminAsync(roleManager, userManager, AdministratorRoleName);
        }

        private static async Task SeedAdminAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, string adminRoleName)
        {
            if (await roleManager.RoleExistsAsync(adminRoleName))
            {
                return;
            }

            var adminRole = new IdentityRole { Name = adminRoleName };

            await roleManager.CreateAsync(adminRole);

            const string adminEmail = "admin@mcd.com";
            const string adminPassword = "Admin123";

            var adminUser = new ApplicationUser()
            {
                Email = adminEmail,
                UserName = adminEmail
            };

            await userManager.CreateAsync(adminUser, adminPassword);
            await userManager.AddToRoleAsync(adminUser, adminRoleName);
        }
    }
}