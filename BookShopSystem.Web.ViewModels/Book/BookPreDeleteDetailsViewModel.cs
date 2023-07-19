using System.ComponentModel.DataAnnotations;

namespace BookShopSystem.Web.ViewModels.Book
{
    public class BookPreDeleteDetailsViewModel
    {
        public string Title { get; set; } = null!;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;
    }
}
