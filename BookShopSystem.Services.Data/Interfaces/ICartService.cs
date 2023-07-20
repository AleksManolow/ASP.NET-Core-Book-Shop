namespace BookShopSystem.Services.Data.Interfaces
{
    public interface ICartService
    {
        public Task<bool> HasBookWithIdAndUserIdInCartAsync(string bookId, string userId);
        public Task AddToCart(string bookId, string userId);
    }
}
