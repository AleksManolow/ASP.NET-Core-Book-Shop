using BookShopSystem.Services.Data.Models.Statistics;

namespace BookShopSystem.Services.Data.Interfaces
{
    public interface IStatisticService
    {
        Task<StatisticsServiceModel> TotalAsync();
    }
}
