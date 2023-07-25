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
        public async Task AddToCartAsync(string bookId, string userId)
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

        public async Task RemoveFromCartAsync(string bookId, string userId)
        {
            var removeCartItem = await dbContext.CartItems.Where(w => w.UserId.ToString() == userId && w.BookId.ToString() == bookId)
                .FirstOrDefaultAsync();

            dbContext.CartItems.Remove(removeCartItem!);
            await dbContext.SaveChangesAsync();
        }
        public async Task<bool> HasUserWithIdEnoughMoneyToBuyBookWithIdAsync(string bookId, string userId)
        {
            var book = await this.dbContext.Books.FirstAsync(b => b.Id.ToString() == bookId);
            var user = await this.dbContext.Users.FirstAsync(u => u.Id.ToString() == userId);

            if (book.Price > user.Wallet)
            {
                return false;
            }
            return true;
        }
        public async Task BuyBookAsync(string bookId, string userId)
        {
            var book = await this.dbContext.Books.Include(b => b.Manager.User).FirstAsync(b => b.Id.ToString() == bookId);
            var user = await this.dbContext.Users.FirstAsync(u => u.Id.ToString() == userId);
            user.Wallet -= book.Price;

            book.Manager.User.Wallet += book.Price;

            Purchase purchase = new Purchase
            {
                UserId = Guid.Parse(userId),
                BookId = Guid.Parse(bookId)
            };

            await dbContext.Purchases.AddAsync(purchase);
            await dbContext.SaveChangesAsync();
        }
    }
}
