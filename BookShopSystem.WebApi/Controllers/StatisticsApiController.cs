using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Services.Data.Models.Statistics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShopSystem.WebApi.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticService statisticService;
        public StatisticsApiController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                StatisticsServiceModel statisticsServiceModel =  await this.statisticService.TotalAsync();
                return this.Ok(statisticsServiceModel);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}
