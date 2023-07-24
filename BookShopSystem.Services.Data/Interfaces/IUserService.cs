namespace BookShopSystem.Services.Data.Interfaces
{
    public interface IUserService
    {
        public Task<UserProfileViewModel> GetUserProfileInfo();
    }
}
