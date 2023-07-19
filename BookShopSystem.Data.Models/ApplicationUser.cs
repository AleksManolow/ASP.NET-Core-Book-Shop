using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopSystem.Data.Models
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }
        [PersonalData]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [PersonalData]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        public decimal Wallet { get; set; }

        [InverseProperty(nameof(Purchase.User))]
        public virtual ICollection<Purchase> Books { get; set; } = new List<Purchase>();

        [InverseProperty(nameof(Wish.User))]
        public virtual ICollection<Wish> Wishes { get; set; } = new List<Wish>();

        [InverseProperty(nameof(CartItem.User))]
        public virtual ICollection<CartItem> BookCarts { get; set; } = new List<CartItem>();
    }
}
