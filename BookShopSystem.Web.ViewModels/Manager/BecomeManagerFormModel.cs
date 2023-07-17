using System.ComponentModel.DataAnnotations;

namespace BookShopSystem.Web.ViewModels.Manager
{
    public class BecomeManagerFormModel
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
