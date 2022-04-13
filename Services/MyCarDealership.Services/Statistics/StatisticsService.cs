namespace MyCarDealership.Services.Statistics
{
    using System.Linq;
    using Data;
    using Models;

    public class StatisticsService : IStatisticsService
    {
        private readonly CarDealershipDbContext data;

        public StatisticsService(CarDealershipDbContext data)
        {
            this.data = data;
        }

        public StatisticsServiceModel Total()
        {
            var totalUsers = this.data.Users.Count();
            var totalPosts = this.data.Posts.Count(p => !p.IsDeleted && p.IsPublic);
            var totalCategories = this.data.Categories.Count();

            return new StatisticsServiceModel
            {
                TotalUsers = totalUsers,
                TotalPosts = totalPosts,
                TotalCategories = totalCategories,
            };
        }
    }
}
