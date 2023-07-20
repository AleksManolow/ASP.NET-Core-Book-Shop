using BookShopSystem.Data;
using BookShopSystem.Data.Models;
using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.ViewModels.Book;
using BookShopSystem.Web.ViewModels.Wish;
using Microsoft.EntityFrameworkCore;
using static BookShopSystem.Common.EntityValidationConstants;

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

        public async Task<IEnumerable<WishListViewModel>> WishlistByUserIdAsync(string userId)
        {
            IEnumerable<WishListViewModel> allUserBooksInWishlist = await this.dbContext
                .Wishes
                .Include(w => w.Book)
                .Where(w => w.UserId.ToString() == userId)
                .Select(w => new WishListViewModel
                {
                    Id = w.Book.Id.ToString(),
                    Title = w.Book.Title,
                    Author = w.Book.Author,
                    ImageUrl = w.Book.ImageUrl,
                    Price = w.Book.Price,
                    AgeRestriction= w.Book.AgeRestriction,
                })
                .ToArrayAsync();
        

            return allUserBooksInWishlist;
        }

        public async Task RemoveFromWishlistAsync(string bookId, string userId)
        {
            var removeWish = await dbContext.Wishes.Where(w => w.UserId.ToString() == userId && w.BookId.ToString() == bookId)
                .FirstOrDefaultAsync();

            dbContext.Wishes.Remove(removeWish!);
            await dbContext.SaveChangesAsync();
        }
    }
}
