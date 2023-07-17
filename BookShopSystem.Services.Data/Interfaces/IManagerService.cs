using BookShopSystem.Web.ViewModels.Manager;

namespace BookShopSystem.Services.Data.Interfaces
{
    public interface IManagerService
    {
        Task<bool> ManagerExistsByUserIdAsync(string userId);
        Task<bool> ManagerExistsByPhoneNumberAsync(string phoneNumber);

        Task<bool> HasBooksByUserIdAsync(string userId);

        Task Create(string userId, BecomeManagerFormModel model);
    }
}
