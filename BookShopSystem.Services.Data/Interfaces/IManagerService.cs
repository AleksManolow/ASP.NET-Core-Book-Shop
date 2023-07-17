namespace BookShopSystem.Services.Data.Interfaces
{
    public interface IManagerService
    {
        Task<bool> ManagerExistsByUserIdAsync(string userId);
    }
}
