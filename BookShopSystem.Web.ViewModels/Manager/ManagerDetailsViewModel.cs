using System.ComponentModel.DataAnnotations;

namespace BookShopSystem.Web.ViewModels.Manager
{
    public class ManagerDetailsViewModel
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; } = null!;

        [Required]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;
    }
}
