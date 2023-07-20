namespace BookShopSystem.Services.Data.Interfaces
{
    public interface ICartService
    {
        public Task<bool> HasBookWithIdInCartAsync(string bookId, string userId);
    }
}
