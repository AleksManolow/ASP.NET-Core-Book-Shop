using BookShopSystem.Services.Data;
using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.Infrastructure.Extensions;
using BookShopSystem.Web.ViewModels.Wish;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static BookShopSystem.Common.NotificationMessagesConstants;

namespace BookShopSystem.Web.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly IWishlistService wishlistService;
        private readonly IManagerService managerService;
        private readonly IBookService bookService;
        public WishlistController(IWishlistService wishlistService, IManagerService managerService, IBookService bookService)
        {
            this.wishlistService = wishlistService;
            this.managerService = managerService;
            this.bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(string bookId)
        {
            bool bookExists = await this.bookService
                .ExistsByIdAsync(bookId);
            if (!bookExists)
            {
                this.TempData[ErrorMessage] = "Book with the provided id does not exist!";

                return this.RedirectToAction("All", "Book");
            }

            bool isUserWish = await this.wishlistService
                .IsUserWithIdWishBookWithIdAsync(bookId, this.User.GetId());
            if (isUserWish)
            {
                this.TempData[ErrorMessage] =
                    "Selected book is already add to wishlist by another user! Please select another book.";

                return this.RedirectToAction("All", "Book");
            }

            bool isUserManager =
                await this.managerService.ManagerExistsByUserIdAsync(this.User.GetId()!);
            if (isUserManager)
            {
                this.TempData[ErrorMessage] = "Managers can't add to wishlist books. Please register as a user!";

                return this.RedirectToAction("Index", "Home");
            }

            try
            {
                await this.wishlistService.AddToWishlistAsync(bookId, this.User.GetId()!);

                TempData[SuccessMessage] = "Successfully added to wishlist!";

                return this.RedirectToAction("MyWishlist", "Wishlist");
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        [HttpGet]
        public async Task<IActionResult> MyWishlist()
        {
            bool isUserManager =
                await this.managerService.ManagerExistsByUserIdAsync(this.User.GetId()!);
            if (isUserManager)
            {
                this.TempData[ErrorMessage] = "Managers has no wishlist. Please register as a user!";

                return this.RedirectToAction("Index", "Home");
            }

            List<WishListViewModel> myWishlist =
                new List<WishListViewModel>();
            try
            {
                myWishlist.AddRange(await this.wishlistService.WishlistByUserIdAsync(this.User.GetId()));
 
                return this.View(myWishlist);
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
