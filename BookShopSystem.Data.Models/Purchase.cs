using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopSystem.Data.Models
{
    public class Purchase
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(Book))]
        public Guid BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
