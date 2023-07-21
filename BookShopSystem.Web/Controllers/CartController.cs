using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.Infrastructure.Extensions;
using BookShopSystem.Web.ViewModels.Cart;
using BookShopSystem.Web.ViewModels.Wish;
using Microsoft.AspNetCore.Mvc;

using static BookShopSystem.Common.NotificationMessagesConstants;

namespace BookShopSystem.Web.Controllers
{
    public class CartController : Controller
    {
        public readonly ICartService cartService;
        public readonly IBookService bookService;
        public readonly IManagerService managerService;

        public CartController(ICartService cartService, IBookService bookService, IManagerService managerService)
        {
            this.cartService = cartService;
            this.bookService = bookService;
            this.managerService = managerService;
        }

        public async Task<IActionResult> MyCart() 
        {
            bool isUserManager =
                await this.managerService.ManagerExistsByUserIdAsync(this.User.GetId()!);
            if (isUserManager)
            {
                this.TempData[ErrorMessage] = "Managers has no cart. Please register as a user!";

                return this.RedirectToAction("Index", "Home");
            }

            List<CartViewModel> myCart =
                new List<CartViewModel>();
            try
            {
                myCart.AddRange(await this.cartService.CartByUserIdAsync(this.User.GetId()));

                return this.View(myCart);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
