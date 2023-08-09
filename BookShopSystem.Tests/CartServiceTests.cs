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
    public class CartServiceTests
    {
        private DbContextOptions<BookShopDbContext> dbOptions;
        private BookShopDbContext dbContext;

        private ICartService cartService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<BookShopDbContext>()
                .UseInMemoryDatabase("BookShopInMemory")
                .Options;
            this.dbContext = new BookShopDbContext(this.dbOptions);
            SeedDatabase(this.dbContext);
            this.cartService = new CartService(this.dbContext);
        }

        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public async Task HasBookWithIdAndUserIdInCartAsyncShouldReturnTrueWhenExists()
        {
            string bookId = Book.Id.ToString();
            string userId = ManagerUser.Id.ToString();

            var result = await this.cartService.HasBookWithIdAndUserIdInCartAsync(bookId, userId);

            Assert.False(result);
        }
        [Test]
        public async Task AddToCartAsyncShouldReturnTrueWhenExists()
        {
            string bookId = Book.Id.ToString();
            string userId = ManagerUser.Id.ToString();

            await this.cartService.AddToCartAsync(bookId, userId);

            var result = await this.cartService.HasBookWithIdAndUserIdInCartAsync(bookId, userId);

            Assert.True(result);
        }
        [Test]
        public async Task RemoveFromCartAsyncShouldReturnTrueWhenExists()
        {
            string bookId = Book.Id.ToString();
            string userId = ManagerUser.Id.ToString();

            await this.cartService.AddToCartAsync(bookId, userId);

            var resultBeforeRemove = await this.cartService.HasBookWithIdAndUserIdInCartAsync(bookId, userId);

            Assert.True(resultBeforeRemove);

            await this.cartService.RemoveFromCartAsync(bookId, userId);

            var resultAfterRemove = await this.cartService.HasBookWithIdAndUserIdInCartAsync(bookId, userId);

            Assert.False(resultAfterRemove);
        }
        [Test]
        public async Task CartByUserIdAsyncShouldReturnTrueWhenExists()
        {
            string bookId = Book.Id.ToString();
            string userId = ManagerUser.Id.ToString();

            await this.cartService.AddToCartAsync(bookId, userId);

            var result = await this.cartService.CartByUserIdAsync(userId);

            Assert.IsTrue(result.Count() == 1);
        }
        [Test]
        public async Task HasUserWithIdEnoughMoneyToBuyBookWithIdAsyncShouldReturnTrueWhenExists()
        {
            string bookId = Book.Id.ToString();
            string userId = BuyerUser.Id.ToString();

            bool result = await this.cartService.HasUserWithIdEnoughMoneyToBuyBookWithIdAsync(bookId, userId);

            Assert.IsTrue(result);
        }
    }
}
