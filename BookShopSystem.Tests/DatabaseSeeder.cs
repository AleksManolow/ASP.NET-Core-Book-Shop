using BookShopSystem.Data;
using BookShopSystem.Data.Models;
using NUnit.Framework.Internal.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.Tests
{
    public static class DatabaseSeeder
    {
        public static ApplicationUser ManagerUser;
        public static ApplicationUser BuyerUser;
        public static Manager Manager;
        public static Book Book;
        public static Purchase Purchase;

        public static void SeedDatabase(BookShopDbContext dbContext)
        {
            ManagerUser = new ApplicationUser()
            {
                UserName = "Pesho",
                NormalizedUserName = "PESHO",
                Email = "pesho@agents.com",
                NormalizedEmail = "PESHO@AGENTS.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
                SecurityStamp = "ca32c787-626e-4234-a4e4-8c94d85a2b1c",
                TwoFactorEnabled = false,
                FirstName = "Pesho",
                LastName = "Petrov"
            };
            BuyerUser = new ApplicationUser()
            {
                UserName = "Gosho",
                NormalizedUserName = "GOSHO",
                Email = "gosho@renters.com",
                NormalizedEmail = "GOSHO@RENTERS.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "8b51706e-f6e8-4dae-b240-54f856fb3004",
                SecurityStamp = "f6af46f5-74ba-43dc-927b-ad83497d0387",
                TwoFactorEnabled = false,
                FirstName = "Gosho",
                LastName = "Goshov",
                Wallet = 100
            };
            Manager = new Manager()
            {
                FirstName = "Pesho",
                LastName = "Petrov",
                PhoneNumber = "+359888888888",
                User = ManagerUser
            };

            Book = new Book()
            {
                Title = "Anna Karenina",
                Author = "Leo Tolstoy",
                Description = "Acclaimed by many as the world's greatest novel, Anna Karenina provides a vast panorama of contemporary life in Russia and of humanity in general.",
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1601352433i/15823480.jpg",
                Price = 20,
                AgeRestriction = 12,
                ReleaseDate = DateTime.Parse("1877-06-26 10:57:31.1728595"),
                GenreId = 1,
                Manager = Manager
            };

            Purchase = new Purchase()
            {
                User = BuyerUser,
                Book = Book
            };

            dbContext.Users.Add(ManagerUser);
            dbContext.Users.Add(BuyerUser);
            dbContext.Managers.Add(Manager);
            dbContext.Books.Add(Book);
            dbContext.Purchases.Add(Purchase);  

            dbContext.SaveChanges();
        }
    }
}
