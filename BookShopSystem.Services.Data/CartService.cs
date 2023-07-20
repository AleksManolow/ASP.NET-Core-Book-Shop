using BookShopSystem.Data;
using BookShopSystem.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookShopSystem.Services.Data 
{
    public class CartService : ICartService
    {
        private readonly BookShopDbContext dbContext;

        public CartService(BookShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> HasBookWithIdInCartAsync(string bookId, string userId)
        {
            return await dbContext.CartItems.AnyAsync(w => w.UserId.ToString() == userId && w.BookId.ToString() == bookId);
        }
    }
}
