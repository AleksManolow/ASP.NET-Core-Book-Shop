using BookShopSystem.Web.ViewModels.User;

namespace BookShopSystem.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileViewModel> GetUserProfileInfo(string id);
        Task AddMoneyToWallet(string id, decimal money);
    }
}
