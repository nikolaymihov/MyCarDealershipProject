namespace MyCarDealershipProject.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Statistics;
    using Services.Statistics.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticsController(IStatisticsService statistics)
        {
            this.statistics = statistics;
        }

        [HttpGet]
        public StatisticsServiceModel GetStatistics()
        {
            return this.statistics.Total();
        }
    }
}