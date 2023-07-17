

using BookShopSystem.Data;
using BookShopSystem.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookShopSystem.Services.Data
{
    public class ManagerService : IManagerService
    {
        private readonly BookShopDbContext dbContext;

        public ManagerService(BookShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> ManagerExistsByUserIdAsync(string userId)
        {
            bool result = await dbContext
                .Managers
                .AnyAsync(a => a.UserId.ToString() == userId);
            return result;
        }
    }
}
