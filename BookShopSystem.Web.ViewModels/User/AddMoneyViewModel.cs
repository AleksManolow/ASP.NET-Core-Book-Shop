using System.ComponentModel.DataAnnotations;

using static BookShopSystem.Common.EntityValidationConstants.User;

namespace BookShopSystem.Web.ViewModels.User
{
    public class AddMoneyViewModel
    {
        [Required]
        [Range(typeof(decimal), AddMoneyToWalletMinLength, AddMoneyToWalletMaxLength)]
        public decimal Money { get; set; }
    }
}
