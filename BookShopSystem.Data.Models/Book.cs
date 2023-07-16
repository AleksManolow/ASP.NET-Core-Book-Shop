using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BookShopSystem.Common.EntityValidationConstants.Book;

namespace BookShopSystem.Data.Models
{
    public class Book
    {
        public Book()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(AuthorMaxLength)]
        public string Author { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrnMaxLength)]
        public string ImageUrn { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int NumberOfSales { get; set; }

        [Required]
        [Range(0, 18)]
        public int AgeRestriction { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; } = null!;

        [ForeignKey(nameof(Manager))]
        public Guid ManagerId { get; set; }
        public virtual Manager Manager { get; set; } = null!;

        [InverseProperty(nameof(Purchase.Book))]
        public virtual ICollection<Purchase> Users { get; set; } = new List<Purchase>();

        [InverseProperty(nameof(Wish.Book))]
        public virtual ICollection<Wish> WishesBook { get; set; } = new List<Wish>();

    }
}
