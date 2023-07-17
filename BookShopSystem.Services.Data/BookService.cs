using BookShopSystem.Data;
using BookShopSystem.Services.Data.Interfaces;
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

        public async Task<IEnumerable<IndexViewModel>> TopThreeSellingBooksAsync()
        {
            IEnumerable<IndexViewModel> topTreeBooks = await this.dbContext
                .Books
                .OrderBy(b => b.NumberOfSales)
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
