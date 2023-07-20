using BookShopSystem.Data;
using BookShopSystem.Data.Models;
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

        public async Task<bool> HasBookWithIdAndUserIdInCartAsync(string bookId, string userId)
        {
            return await dbContext.CartItems.AnyAsync(w => w.UserId.ToString() == userId && w.BookId.ToString() == bookId);
        }
        public async Task AddToCart(string bookId, string userId)
        {
            CartItem item = new CartItem()
            {
                UserId = Guid.Parse(userId),
                BookId = Guid.Parse(bookId)
            };

            await this.dbContext.CartItems.AddAsync(item);
            await dbContext.SaveChangesAsync();
        }
    }
}
