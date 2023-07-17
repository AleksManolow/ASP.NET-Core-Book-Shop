using BookShopSystem.Data;
using BookShopSystem.Data.Models;
using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.ViewModels.Book;
using BookShopSystem.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;

namespace BookShopSystem.Services.Data
{
    public class BookService : IBookService
    {
        private readonly BookShopDbContext dbContext;

        public BookService(BookShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateAndReturnIdAsync(BookFormModel model, string managerId)
        {
            Book newBook = new Book()
            {
                Title = model.Title,
                Author= model.Author,
                Description= model.Description,
                ImageUrl = model.ImageUrl,
                AgeRestriction=model.AgeRestriction,
                Price =model.Price,
                GenreId =model.GenreId,
                ManagerId = Guid.Parse(managerId)
            };

            await dbContext.Books.AddAsync(newBook);
            await dbContext.SaveChangesAsync();
            return newBook.Id.ToString();
        }

        public async Task<IEnumerable<IndexViewModel>> TopThreeSellingBooksAsync()
        {
            IEnumerable<IndexViewModel> topTreeBooks = await this.dbContext
                .Books
                .OrderByDescending(b => b.NumberOfSales)
                .ThenBy(b => b.ReleaseDate)
                .Take(3)
                .Select(b => new IndexViewModel()
                {
                    Id = b.Id.ToString(),
                    Title = b.Title,
                    ImageUrl = b.ImageUrl
                })
                .ToArrayAsync();

            return topTreeBooks;
        }
    }
}
