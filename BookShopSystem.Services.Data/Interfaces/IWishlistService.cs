using BookShopSystem.Web.ViewModels.Wish;

namespace BookShopSystem.Services.Data.Interfaces
{
    public interface IWishlistService
    {
        Task<bool> IsUserWithIdWishBookWithIdAsync(string bookId, string userId);
        Task AddToWishlistAsync(string bookId, string userId);

        /* Task<bool> RemoveFromWishlistA(string userId, string bookId);

        Task<bool> MoveToCart(string userId, string bookId);

        Task<IEnumerable<WishListViewModel>> UsersWishlist(string userId);*/
    }
}
