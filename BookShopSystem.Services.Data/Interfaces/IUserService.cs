using BookShopSystem.Web.Areas.Admin.ViewModels.User;
using BookShopSystem.Web.ViewModels.User;

namespace BookShopSystem.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileViewModel> GetUserProfileInfo(string id);
        Task AddMoneyToWallet(string id, decimal money);
        Task<string> GetFullNameByIdAsync(string userId);
        Task<IEnumerable<UserViewModel>> AllAsync();
    }
}
