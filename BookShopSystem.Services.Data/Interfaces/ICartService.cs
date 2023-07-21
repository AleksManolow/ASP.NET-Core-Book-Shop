using BookShopSystem.Web.ViewModels.Cart;

namespace BookShopSystem.Services.Data.Interfaces
{
    public interface ICartService
    {
        Task<bool> HasBookWithIdAndUserIdInCartAsync(string bookId, string userId);
        Task AddToCart(string bookId, string userId);
        Task<IEnumerable<CartViewModel>> CartByUserIdAsync(string userId);
        Task RemoveFromCartAsync(string bookId, string userId);
    }
}
