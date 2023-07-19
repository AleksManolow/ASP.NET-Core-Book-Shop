using BookShopSystem.Services.Data.Models.Book;
using BookShopSystem.Web.ViewModels.Book;
using BookShopSystem.Web.ViewModels.Home;

namespace BookShopSystem.Services.Data.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<IndexViewModel>> TopThreeSellingBooksAsync();
        Task<string> CreateAndReturnIdAsync(BookFormModel model, string managerId);

        Task<AllBooksFilteredAndPagedServiceModel> AllAsync(AllBookQueryModel queryModel);
        Task<bool> ExistsByIdAsync(string bookId);
        Task<BookDetailsViewModel> GetDetailsByIdAsync(string bookId);
        Task<bool> IsBoughtByUserWithIdAsync(string bookId, string userId);
        Task<BookPreDeleteDetailsViewModel> GetBookForDeleteByIdAsync(string bookId);
        Task<bool> IsManagerWithIdSellerOfBookWithIdAsync(string bookId, string managerId);
        Task DeleteHouseByIdAsync(string bookId);
        Task<BookFormModel> GetBookForEditByIdAsync(string bookId);
        Task EditBookByIdAndFormModelAsync(string bookId, BookFormModel formModel);
        Task<IEnumerable<BookAllViewModel>> AllByManagerIdAsync(string managerId);
        Task<IEnumerable<BookAllViewModel>> AllByUserIdAsync(string userId);

    }
}
