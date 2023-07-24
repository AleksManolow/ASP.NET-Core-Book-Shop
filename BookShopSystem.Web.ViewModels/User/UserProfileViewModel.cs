namespace BookShopSystem.Web.ViewModels.User
{
    public class UserProfileViewModel
    {
        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public decimal Wallet { get; set; }
    }
}
