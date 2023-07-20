using BookShopSystem.Web.ViewModels.Wish;

namespace BookShopSystem.Services.Data.Interfaces
{
    public interface IWishlistService
    {
        Task<bool> IsUserWithIdWishBookWithIdAsync(string bookId, string userId);
        Task AddToWishlistAsync(string bookId, string userId);
        Task<IEnumerable<WishListViewModel>> WishlistByUserIdAsync(string userId);

        Task RemoveFromWishlistAsync(string bookId, string userId);

        //Task<bool> MoveToCart(string userId, string bookId);

    }
}
