using BookShopSystem.Data;
using BookShopSystem.Data.Models;
using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.ViewModels.Cart;
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

        public async Task<IEnumerable<CartViewModel>> CartByUserIdAsync(string userId)
        {
            IEnumerable<CartViewModel> allUserBooksInCart = await this.dbContext
                .CartItems
                .Include(c => c.Book)
                .Where(c => c.UserId.ToString() == userId)
                .Select(c => new CartViewModel
                {
                    Id = c.Book.Id.ToString(),
                    Title = c.Book.Title,
                    Author = c.Book.Author,
                    ImageUrl = c.Book.ImageUrl,
                    Price = c.Book.Price,
                    AgeRestriction = c.Book.AgeRestriction,
                })
                .ToArrayAsync();


            return allUserBooksInCart;
        }
    }
}
