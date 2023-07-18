using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookShopSystem.Web.ViewModels.Book
{
    public class BookAllViewModel
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
        [Display(Name = "Age restriction")]
        public int AgeRestriction { get; set; }

    }
}
