using BookShopSystem.Web.ViewModels.Manager;
using System.ComponentModel.DataAnnotations;

namespace BookShopSystem.Web.ViewModels.Book
{
    public class BookDetailsViewModel : BookAllViewModel
    {
        public string Description { get; set; } = null!;

        public string Genre { get; set; } = null!;

        [Display(Name = "Number Of Sales")]
        public int NumberOfSales { get; set; }

        public ManagerDetailsViewModel Manager { get; set; } = null!;
    }
}
