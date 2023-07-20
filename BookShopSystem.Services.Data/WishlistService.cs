using BookShopSystem.Data;
using BookShopSystem.Data.Models;
using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.ViewModels.Wish;
using Microsoft.EntityFrameworkCore;

namespace BookShopSystem.Services.Data
{
    public class WishlistService : IWishlistService
    {
        private readonly BookShopDbContext dbContext;

        public WishlistService(BookShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> IsUserWithIdWishBookWithIdAsync(string bookId, string userId)
        {
            return await dbContext.Wishes.AnyAsync(w => w.UserId.ToString() == userId && w.BookId.ToString() == bookId);
        }
        public async Task AddToWishlistAsync(string bookId, string userId)
        {
            Wish wish = new Wish()
            {
                BookId = Guid.Parse(bookId),
                UserId = Guid.Parse(userId)
            };
            await this.dbContext.Wishes.AddAsync(wish);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
