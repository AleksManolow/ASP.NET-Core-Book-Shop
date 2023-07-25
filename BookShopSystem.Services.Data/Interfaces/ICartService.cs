using BookShopSystem.Web.ViewModels.Cart;
using static BookShopSystem.Common.EntityValidationConstants;

namespace BookShopSystem.Services.Data.Interfaces
{
    public interface ICartService
    {
        Task<bool> HasBookWithIdAndUserIdInCartAsync(string bookId, string userId);
        Task AddToCartAsync(string bookId, string userId);
        Task<IEnumerable<CartViewModel>> CartByUserIdAsync(string userId);
        Task RemoveFromCartAsync(string bookId, string userId);
        Task<bool> HasUserWithIdEnoughMoneyToBuyBookWithIdAsync(string bookId, string userId);
        Task BuyBookAsync(string bookId, string userId);
    }
}
