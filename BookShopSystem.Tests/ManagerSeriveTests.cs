using BookShopSystem.Data;
using BookShopSystem.Services.Data;
using BookShopSystem.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

using static BookShopSystem.Tests.DatabaseSeeder;

namespace BookShopSystem.Tests
{
    public class ManagerSeriveTests
    {
        private DbContextOptions<BookShopDbContext> dbOptions;
        private BookShopDbContext dbContext;

        private IManagerService managerService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<BookShopDbContext>()
                .UseInMemoryDatabase("BookShopInMemory")
                .Options;
            this.dbContext = new BookShopDbContext(this.dbOptions);
            SeedDatabase(this.dbContext);
            this.managerService = new ManagerService(this.dbContext);
        }


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ManagerExistsByUserIdAsyncShouldReturnTrueWhenExists()
        {
            string existingManagerUserId = ManagerUser.Id.ToString();

            bool result = await this.managerService.ManagerExistsByUserIdAsync(existingManagerUserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ManagerExistsByPhoneNumberAsyncShouldReturnTrueWhenExists()
        {
            string existingManagerPhoneNumber = Manager.PhoneNumber;

            bool result = await this.managerService.ManagerExistsByPhoneNumberAsync(existingManagerPhoneNumber);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetManagerIdByUserIdAsyncShouldReturnTrueWhenExists()
        {
            string existingManagerUserId = ManagerUser.Id.ToString();

            string managerId = await this.managerService.GetManagerIdByUserIdAsync(existingManagerUserId);

            Assert.NotNull(managerId);
        }
        [Test]
        public async Task HasBookWithIdAsyncShouldReturnTrueWhenExists()
        {
            string existingManagerId = Manager.UserId.ToString();

            string book = Book.Id.ToString();

            bool result = await this.managerService.HasBookWithIdAsync(existingManagerId, book);

            Assert.IsTrue(result);
        }
        //HasBooksByUserIdAsync
        [Test]
        public async Task HasBooksByUserIdAsyncShouldReturnTrueWhenExists()
        {
            string existingBuyerUserId = BuyerUser.Id.ToString();

            bool result = await this.managerService.HasBooksByUserIdAsync(existingBuyerUserId);

            Assert.IsTrue(result);
        }
    }
}