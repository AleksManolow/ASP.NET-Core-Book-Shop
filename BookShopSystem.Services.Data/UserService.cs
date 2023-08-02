using BookShopSystem.Data;
using BookShopSystem.Data.Models;
using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.Areas.Admin.ViewModels.User;
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
        public async Task AddMoneyToWallet(string id, decimal money)
        {
            var user = await this.dbContext.Users.FirstOrDefaultAsync(u => u.Id.ToString() == id);
            user!.Wallet += money;

            await dbContext.SaveChangesAsync();
        }

        public async Task<string> GetFullNameByIdAsync(string userId)
        {
            ApplicationUser? user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }

        public async Task<IEnumerable<UserViewModel>> AllAsync()
        {
            List<UserViewModel> allUsers = await this.dbContext
                .Users
                .Select(u => new UserViewModel()
                {
                    Id = u.Id.ToString(),
                    Email = u.Email,
                    FullName = u.FirstName + " " + u.LastName
                })
                .ToListAsync();

            foreach (UserViewModel user in allUsers)
            {
                Manager? manager = this.dbContext
                    .Managers
                    .FirstOrDefault(a => a.UserId.ToString() == user.Id);

                if (manager != null)
                {
                    user.PhoneNumber = manager.PhoneNumber;
                }
                else
                {
                    user.PhoneNumber = string.Empty;
                }
            }

            return allUsers;
        }
    }
}
