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
using BookShopSystem.Web.ViewModels.User;

namespace BookShopSystem.Tests
{
    public class UserServiceTests
    {
        private DbContextOptions<BookShopDbContext> dbOptions;
        private BookShopDbContext dbContext;

        private IUserService userService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<BookShopDbContext>()
                .UseInMemoryDatabase("BookShopInMemory")
                .Options;
            this.dbContext = new BookShopDbContext(this.dbOptions);
            SeedDatabase(this.dbContext);
            this.userService = new UserService(this.dbContext);
        }

        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public async Task GetUserProfileInfoShouldReturnTrueWhenExists() 
        {
            string userId = ManagerUser.Id.ToString();

            var profile = await this.userService.GetUserProfileInfo(userId);

            Assert.True(profile != null);
            Assert.IsInstanceOf(typeof(UserProfileViewModel), profile);
        }
        [Test]
        public async Task AddMoneyToWalletShouldReturnTrueWhenExists()
        {
            string userId = BuyerUser.Id.ToString();

            await this.userService.AddMoneyToWallet(userId, 20);

            Assert.True(BuyerUser.Wallet == 120);
        }
        [Test]
        public async Task GetFullNameByIdAsyncShouldReturnTrueWhenExists()
        {
            string userId = BuyerUser.Id.ToString();

            string result = await this.userService.GetFullNameByIdAsync(userId);

            Assert.True(result != null);
            Assert.True(result == $"{BuyerUser.FirstName} {BuyerUser.LastName}");
        }
        [Test]
        public async Task AllAsyncShouldReturnTrueWhenExists()
        {
            string userId = BuyerUser.Id.ToString();

            var result = await this.userService.AllAsync();

            Assert.True(result.Count() == 2);
        }
    }
}
