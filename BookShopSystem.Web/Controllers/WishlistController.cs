﻿using BookShopSystem.Services.Data;
using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.Infrastructure.Extensions;
using BookShopSystem.Web.ViewModels.Wish;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using static BookShopSystem.Common.NotificationMessagesConstants;

namespace BookShopSystem.Web.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly IWishlistService wishlistService;
        private readonly IManagerService managerService;
        private readonly IBookService bookService;
        private readonly ICartService cartService;
        public WishlistController(IWishlistService wishlistService, IManagerService managerService, IBookService bookService, ICartService cartService)
        {
            this.wishlistService = wishlistService;
            this.managerService = managerService;
            this.bookService = bookService;
            this.cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(string id)
        {
            bool bookExists = await this.bookService
                .ExistsByIdAsync(id);
            if (!bookExists)
            {
                this.TempData[ErrorMessage] = "Book with the provided id does not exist!";

                return this.RedirectToAction("All", "Book");
            }

            bool isUserWish = await this.wishlistService
                .IsUserWithIdWishBookWithIdAsync(id, this.User.GetId());
            if (isUserWish)
            {
                this.TempData[ErrorMessage] =
                    "Selected book is already add to wishlist by you! Please select another book.";

                return this.RedirectToAction("All", "Book");
            }

            bool isUserCart = await this.cartService
                .HasBookWithIdAndUserIdInCartAsync(id, this.User.GetId());
            if (isUserCart)
            {
                this.TempData[ErrorMessage] =
                    "Selected book is already add to cart by you! You can just buy it.";

                return this.RedirectToAction("MyCart", "Cart");
            }

            bool isUserManager =
                await this.managerService.ManagerExistsByUserIdAsync(this.User.GetId()!);
            if (isUserManager && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "Managers can't add to wishlist books. Please register as a user!";

                return this.RedirectToAction("Index", "Home");
            }

            try
            {
                await this.wishlistService.AddToWishlistAsync(id, this.User.GetId()!);

                TempData[SuccessMessage] = "Successfully added to wishlist!";

                return this.RedirectToAction("All", "Book");
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
            if (isUserManager && !this.User.IsAdmin())
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
        [HttpPost]
        public async Task<IActionResult> RemoveFromWishlist(string id)
        {
            bool bookExists = await this.bookService
                .ExistsByIdAsync(id);
            if (!bookExists)
            {
                this.TempData[ErrorMessage] = "Book with the provided id does not exist!";

                return this.RedirectToAction("All", "Book");
            }

            bool isUserWish = await this.wishlistService
                .IsUserWithIdWishBookWithIdAsync(id, this.User.GetId());
            if (!isUserWish)
            {
                this.TempData[ErrorMessage] = "The selected book not on this user's wishlist!";

                return this.RedirectToAction("All", "Book");
            }

            bool isUserManager =
                await this.managerService.ManagerExistsByUserIdAsync(this.User.GetId()!);
            if (isUserManager && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "Managers can't remove from wishlist books. Please register as a user!";

                return this.RedirectToAction("Index", "Home");
            }

            try
            {
                await this.wishlistService.RemoveFromWishlistAsync(id, this.User.GetId()!);

                TempData[SuccessMessage] = "Successfully removed from wishlist!";

                return this.RedirectToAction("MyWishlist", "Wishlist");
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

        }
        [HttpPost]
        public async Task<IActionResult> MoveToCart(string id)
        {
            bool bookExists = await this.bookService
                .ExistsByIdAsync(id);
            if (!bookExists)
            {
                this.TempData[ErrorMessage] = "Book with the provided id does not exist!";

                return this.RedirectToAction("All", "Book");
            }

            bool isUserWish = await this.wishlistService
                .IsUserWithIdWishBookWithIdAsync(id, this.User.GetId());
            if (!isUserWish)
            {
                this.TempData[ErrorMessage] =
                    "The selected book not on this user's wishlist!";

                return this.RedirectToAction("All", "Book");
            }

            bool isUserManager =
                await this.managerService.ManagerExistsByUserIdAsync(this.User.GetId()!);
            if (isUserManager && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "Managers can't move to cart. Please register as a user!";

                return this.RedirectToAction("Index", "Home");
            }

            bool isBookInCart =
                await this.cartService.HasBookWithIdAndUserIdInCartAsync(id, this.User.GetId());

            if (isBookInCart)
            {
                this.TempData[ErrorMessage] = "This book has already been added!";

                return this.RedirectToAction("MyWishlist", "Wishlist");
            }

            try
            {
                await this.wishlistService.RemoveFromWishlistAsync(id, this.User.GetId()!);
                await this.cartService.AddToCartAsync(id, this.User.GetId()!);

                TempData[SuccessMessage] = "Successfully move to cart!";

                return this.RedirectToAction("MyWishlist", "Wishlist");
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
