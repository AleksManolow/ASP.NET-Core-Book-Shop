using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookShopSystem.Web.ViewModels.Wish
{
    public class WishListViewModel
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
