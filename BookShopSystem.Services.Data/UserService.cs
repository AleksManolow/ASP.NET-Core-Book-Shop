using BookShopSystem.Data;
using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.ViewModels.User;
using Microsoft.EntityFrameworkCore;

namespace BookShopSystem.Services.Data
{
    public class UserService:IUserService
    {
        public readonly BookShopDbContext dbContext;
        public UserService(BookShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserProfileViewModel> GetUserProfileInfo(string id)
        {
            return await this.dbContext.Users
                .Where(u => u.Id.ToString() == id)
                .Select(u => new UserProfileViewModel
                {
                    Id = u.Id.ToString(),
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Wallet = u.Wallet,
                })
                .FirstAsync();
        }
    }
}
