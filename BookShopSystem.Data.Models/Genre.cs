using System.ComponentModel.DataAnnotations;
using static BookShopSystem.Common.EntityValidationConstants.Genre;

namespace BookShopSystem.Data.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}