using BookShopSystem.Data;
using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Services.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static BookShopSystem.Tests.DatabaseSeeder;

namespace BookShopSystem.Tests
{
    public class WishlistServiceTests
    {
        private DbContextOptions<BookShopDbContext> dbOptions;
        private BookShopDbContext dbContext;

        private IWishlistService wishlistService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<BookShopDbContext>()
                .UseInMemoryDatabase("BookShopInMemory")
                .Options;
            this.dbContext = new BookShopDbContext(this.dbOptions);
            SeedDatabase(this.dbContext);
            this.wishlistService = new WishlistService(this.dbContext);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task IsUserWithIdWishBookWithIdAsyncShouldReturnTrueWhenExists()
        {
            string bookId = Book.Id.ToString();
            string userId = ManagerUser.Id.ToString();

            var result = await this.wishlistService.IsUserWithIdWishBookWithIdAsync(bookId, userId);

            Assert.False(result);   
        }
        [Test]
        public async Task AddToWishlistAsyncShouldReturnTrueWhenExists()
        {
            string bookId = Book.Id.ToString();
            string userId = ManagerUser.Id.ToString();

            await this.wishlistService.AddToWishlistAsync(bookId, userId);

            var result = await this.wishlistService.IsUserWithIdWishBookWithIdAsync(bookId, userId);

            Assert.True(result);
        }
        [Test]
        public async Task RemoveFromWishlistAsyncShouldReturnTrueWhenExists()
        {
            string bookId = Book.Id.ToString();
            string userId = ManagerUser.Id.ToString();

            await this.wishlistService.AddToWishlistAsync(bookId, userId);

            var resultBeforeRemove = await this.wishlistService.IsUserWithIdWishBookWithIdAsync(bookId, userId);

            Assert.True(resultBeforeRemove);

            await this.wishlistService.RemoveFromWishlistAsync(bookId, userId);

            var resultAfterRemove = await this.wishlistService.IsUserWithIdWishBookWithIdAsync(bookId, userId);

            Assert.False(resultAfterRemove);
        }
        [Test]
        public async Task WishlistByUserIdAsyncShouldReturnTrueWhenExists()
        {
            string bookId = Book.Id.ToString();
            string userId = ManagerUser.Id.ToString();

            await this.wishlistService.AddToWishlistAsync(bookId, userId);

            var result = await this.wishlistService.WishlistByUserIdAsync(userId);

            Assert.IsTrue(result.Count() == 1);
        }
    }
}
