using BookShopSystem.Web.ViewModels.Book.Enums;
using System.ComponentModel.DataAnnotations;

using static BookShopSystem.Common.GeneralApplicationConstants;

namespace BookShopSystem.Web.ViewModels.Book
{
    public class AllBookQueryModel
    {
        public AllBookQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.BookPerPage = EntitiesPerPage;

            this.Genries = new HashSet<string>();
            this.Books = new HashSet<BookAllViewModel>();
        }
        public string? Genre { get; set; }

        [Display(Name = "Search By Word")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Book By")]
        public BookSorting BookSorting { get; set; }

        public int CurrentPage { get; set; }
        [Display(Name = "Show Book On Page")]
        public int BookPerPage { get; set; }

        public int TotalBooks { get; set; }

        public IEnumerable<string> Genries { get; set; }

        public IEnumerable<BookAllViewModel> Books { get; set; }
    }
}
