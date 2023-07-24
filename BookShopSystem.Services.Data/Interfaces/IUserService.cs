using BookShopSystem.Web.ViewModels.User;

namespace BookShopSystem.Services.Data.Interfaces
{
    public interface IUserService
    {
        public Task<UserProfileViewModel> GetUserProfileInfo(string id);
    }
}
