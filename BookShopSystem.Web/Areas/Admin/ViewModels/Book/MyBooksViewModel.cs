using BookShopSystem.Web.ViewModels.Book;

namespace BookShopSystem.Web.Areas.Admin.ViewModels.Book
{
    public class MyBooksViewModel
    {
        public IEnumerable<BookAllViewModel> AddedBooks { get; set; } = null!;
        public IEnumerable<BookAllViewModel> BoughtBooks { get;set; } = null!;
    }
}
