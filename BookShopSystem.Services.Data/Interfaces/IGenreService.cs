using BookShopSystem.Web.ViewModels.Genre;

namespace BookShopSystem.Services.Data.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<BookSelectGenreFormModel>> AllGenriesAsync();
        Task<bool> ExistsByIdAsync(int id);
    }
}
