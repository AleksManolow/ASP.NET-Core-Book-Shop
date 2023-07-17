using BookShopSystem.Web.ViewModels.Genre;
using System.ComponentModel.DataAnnotations;

using static BookShopSystem.Common.EntityValidationConstants.Book;

namespace BookShopSystem.Web.ViewModels.Book
{
    public class BookFormModel
    {
        public BookFormModel()
        {
            this.Genries = new HashSet<BookSelectGenreFormModel>();
        }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(AuthorMaxLength, MinimumLength = AuthorMinLength)]
        public string Author { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(ImageUrlMaxLength)]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Range(typeof(decimal), PriceMinLength, PriceMaxLength)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 18)]
        public int AgeRestriction { get; set; }

        [Required]
        public string ReleaseDate { get; set; } = null!;

        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        [Required]
        public IEnumerable<BookSelectGenreFormModel> Genries { get; set; }
    }
}
