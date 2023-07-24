using BookShopSystem.Data;
using BookShopSystem.Services.Data.Interfaces;

namespace BookShopSystem.Services.Data
{
    public class UserService:IUserService
    {
        public readonly BookShopDbContext dbContext;
        public UserService(BookShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserProfileViewModel> GetUserProfileInfo()
        {
            throw new NotImplementedException();
        }
    }
}
