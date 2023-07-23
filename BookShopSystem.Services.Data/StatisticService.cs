using BookShopSystem.Data;
using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Services.Data.Models.Statistics;
using Microsoft.EntityFrameworkCore;

namespace BookShopSystem.Services.Data
{
    public class StatisticService : IStatisticService
    {
        public readonly BookShopDbContext bookShopDbContext;
        public StatisticService(BookShopDbContext bookShopDbContext)
        {
            this.bookShopDbContext = bookShopDbContext;
        }
        public async Task<StatisticsServiceModel> TotalAsync()
        {
            return new StatisticsServiceModel
            {
                TotalPurchases = await bookShopDbContext.Purchases.CountAsync(),
                TotalFantasyPurchases = await bookShopDbContext.Purchases.CountAsync(p => p.Book.Genre.Name == "Fantasy"),
                TotalSciFiPurchases = await bookShopDbContext.Purchases.CountAsync(p => p.Book.Genre.Name == "SciFi"),
                TotalMysteryPurchases = await bookShopDbContext.Purchases.CountAsync(p => p.Book.Genre.Name == "Mystery"),
                TotalThrillerPurchases = await bookShopDbContext.Purchases.CountAsync(p => p.Book.Genre.Name == "Thriller"),
                TotalRomancePurchases = await bookShopDbContext.Purchases.CountAsync(p => p.Book.Genre.Name == "Romance"),
                TotalWesternsPurchases = await bookShopDbContext.Purchases.CountAsync(p => p.Book.Genre.Name == "Westerns"),
                TotalDystopianPurchases = await bookShopDbContext.Purchases.CountAsync(p => p.Book.Genre.Name == "Dystopian"),
                TotalContemporaryPurchases = await bookShopDbContext.Purchases.CountAsync(p => p.Book.Genre.Name == "Contemporary"),
            };
        }
    }
}
