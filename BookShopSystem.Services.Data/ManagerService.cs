﻿

using BookShopSystem.Data;
using BookShopSystem.Data.Models;
using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.ViewModels.Manager;
using Microsoft.EntityFrameworkCore;

namespace BookShopSystem.Services.Data
{
    public class ManagerService : IManagerService
    {
        private readonly BookShopDbContext dbContext;

        public ManagerService(BookShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ManagerExistsByUserIdAsync(string userId)
        {
            bool result = await dbContext
                .Managers
                .AnyAsync(a => a.UserId.ToString() == userId);
            return result;
        }

        public async Task<bool> ManagerExistsByPhoneNumberAsync(string phoneNumber)
        {
            bool result = await this.dbContext
                .Managers
                .AnyAsync(a => a.PhoneNumber == phoneNumber);

            return result;
        }

        public async Task<bool> HasBooksByUserIdAsync(string userId)
        {
            return await this.dbContext.Purchases.AnyAsync(u => u.UserId.ToString() == userId);
        }

        public async Task Create(string userId, BecomeManagerFormModel model)
        {
            ApplicationUser user = await this.dbContext
                .Users
                .FirstAsync(u => u.Id.ToString() == userId);

            Manager newManager = new Manager()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = model.PhoneNumber,
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.Managers.AddAsync(newManager);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<string> GetManagerIdByUserIdAsync(string userId)
        {
            Manager? manager = await this.dbContext
                .Managers
                .FirstOrDefaultAsync(m => m.UserId.ToString() == userId);

            return manager!.Id.ToString();
        }

        public async Task<bool> HasBookWithIdAsync(string? userId, string bookId)
        {
            Manager? manager = await this.dbContext
                .Managers
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.UserId.ToString() == userId);
            if (manager == null)
            {
                return false;
            }

            bookId = bookId.ToLower();
            return manager.Books.Any(h => h.Id.ToString() == bookId);
        }
    }
}
