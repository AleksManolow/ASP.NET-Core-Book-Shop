using BookShopSystem.Data;
using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.ViewModels.Genre;
using Microsoft.EntityFrameworkCore;

namespace BookShopSystem.Services.Data
{
    public class GenreService : IGenreService
    {
        private readonly BookShopDbContext dbContext;
        public GenreService(BookShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<BookSelectGenreFormModel>> AllGenriesAsync()
        {
            IEnumerable<BookSelectGenreFormModel> bookSelectGenreFormModels = await this.dbContext
                .Genres
                .AsNoTracking()
                .Select(g => new BookSelectGenreFormModel()
                {
                    Id= g.Id,
                    Name = g.Name,
                })
                .ToArrayAsync();
            return bookSelectGenreFormModels;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool result = await this.dbContext
                .Genres
                .AnyAsync(c => c.Id == id);

            return result;
        }
    }
}
