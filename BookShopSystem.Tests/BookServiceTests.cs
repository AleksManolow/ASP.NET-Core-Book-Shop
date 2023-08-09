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
using BookShopSystem.Web.ViewModels.Book;

namespace BookShopSystem.Tests
{
    public class BookServiceTests
    {
        private DbContextOptions<BookShopDbContext> dbOptions;
        private BookShopDbContext dbContext;

        private IBookService bookService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<BookShopDbContext>()
                .UseInMemoryDatabase("BookShopInMemory")
                .Options;
            this.dbContext = new BookShopDbContext(this.dbOptions);
            SeedDatabase(this.dbContext);
            this.bookService = new BookService(this.dbContext);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ExistsByIdAsyncShouldReturnTrueWhenExists()
        {
            string bookId = Book.Id.ToString();

            bool result = await this.bookService.ExistsByIdAsync(bookId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetDetailsByIdAsyncShouldReturnTrueWhenExists()
        {
            string bookId = Book.Id.ToString();

            var result = await this.bookService.GetDetailsByIdAsync(bookId);

            Assert.That(bookId, Is.EqualTo(result.Id.ToString()));
        }
        [Test]
        public async Task IsBoughtByUserWithIdAsyncShouldReturnTrueWhenExists()
        {
            string bookId = Book.Id.ToString();
            string userId = BuyerUser.Id.ToString();

            var result = await this.bookService.IsBoughtByUserWithIdAsync(bookId, userId);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task TopThreeSellingBooksAsyncShouldReturnTrueWhenExists()
        {
            var result = await this.bookService.TopThreeSellingBooksAsync();

            Assert.True(result.Count() == 3);
        }
        [Test]
        public async Task IsManagerWithIdSellerOfBookWithIdAsyncShouldReturnTrueWhenExists()
        {
            string bookId = Book.Id.ToString();
            string mangerId = Manager.Id.ToString();

            var result = await this.bookService.IsManagerWithIdSellerOfBookWithIdAsync(bookId, mangerId);

            Assert.True(result);
        }
        [Test]
        public async Task GetBookForDeleteByIdAsyncShouldReturnTrueWhenExists()
        {
            string bookId = Book.Id.ToString();

            var result = await this.bookService.GetBookForDeleteByIdAsync(bookId);

            Assert.IsInstanceOf(typeof(BookPreDeleteDetailsViewModel), result);
            Assert.That(Book.Title, Is.EqualTo(result.Title));
        }
        [Test]
        public async Task DeleteBookByIdAsync()
        {
            string bookId = Book.Id.ToString();

            await this.bookService.DeleteBookByIdAsync(bookId);

            Assert.False(Book.IsActive);
        }
        [Test] 
        public async Task EditBookShouldReturnTrueWhenExists()
        {
            string bookId = Book.Id.ToString();

            var bookToEdit = await this.bookService.GetBookForEditByIdAsync(bookId);

            bookToEdit.Title = "ChangeTitle";

            await this.bookService.EditBookByIdAndFormModelAsync(bookId, bookToEdit);

            Assert.True(Book.Title == "ChangeTitle");
        }
        [Test]
        public async Task AllByManagerIdAsyncShouldReturnTrueWhenExists()
        {
            string managerId = Manager.Id.ToString();  

            var result = await this.bookService.AllByManagerIdAsync(managerId);

            Assert.True(result.Count() == 1);
        }
        [Test]
        public async Task AllByUserIdAsyncShouldReturnTrueWhenExists()
        {
            string userId = BuyerUser.Id.ToString();

            var result = await this.bookService.AllByUserIdAsync(userId);

            Assert.True(result.Count() == 1);
        }

    }
}
