using BookShopSystem.Web.ViewModels.Book;

namespace BookShopSystem.Services.Data.Models.Book
{
    public class AllBooksFilteredAndPagedServiceModel
    {
        public AllBooksFilteredAndPagedServiceModel()
        {
            this.Books = new HashSet<BookAllViewModel>();
        }
        public int TotalBooksCount { get; set; }
        public IEnumerable<BookAllViewModel> Books { get; set; }
    }
}
