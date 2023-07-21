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
        public readonly IWishlistService wishlistService;

        public CartController(ICartService cartService, IBookService bookService, IManagerService managerService, IWishlistService wishlistService)
        {
            this.cartService = cartService;
            this.bookService = bookService;
            this.managerService = managerService;
            this.wishlistService = wishlistService;
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
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(string id)
        {
            bool bookExists = await this.bookService
                .ExistsByIdAsync(id);
            if (!bookExists)
            {
                this.TempData[ErrorMessage] = "Book with the provided id does not exist!";

                return this.RedirectToAction("All", "Book");
            }

            bool isUserWish = await this.cartService
                .HasBookWithIdAndUserIdInCartAsync(id, this.User.GetId());
            if (!isUserWish)
            {
                this.TempData[ErrorMessage] = "The selected book not on this user's cart!";

                return this.RedirectToAction("All", "Book");
            }

            bool isUserManager =
                await this.managerService.ManagerExistsByUserIdAsync(this.User.GetId()!);
            if (isUserManager)
            {
                this.TempData[ErrorMessage] = "Managers can't remove from cart books. Please register as a user!";

                return this.RedirectToAction("Index", "Home");
            }

            try
            {
                await this.cartService.RemoveFromCartAsync(id, this.User.GetId()!);

                TempData[SuccessMessage] = "Successfully removed from cart!";

                return this.RedirectToAction("MyCart", "Cart");
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
