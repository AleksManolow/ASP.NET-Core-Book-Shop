using BookShopSystem.Services.Data;
using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.Infrastructure.Extensions;
using BookShopSystem.Web.ViewModels.Book;
using BookShopSystem.Web.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

using static BookShopSystem.Common.NotificationMessagesConstants;

namespace BookShopSystem.Web.Controllers
{
    public class UserController : Controller
    {
        public readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> Profile() 
        {
            if (string.IsNullOrWhiteSpace(this.User.GetId()))
            {
                this.TempData[ErrorMessage] = "You are not logged in!";
                return View("Error404");
            }
            return View(await userService.GetUserProfileInfo(this.User.GetId()));  
        }
        [HttpGet]
        public async Task<IActionResult> AddMoney()
        {
            return View(new AddMoneyViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> AddMoney(AddMoneyViewModel model)
        {
            try
            {
                await this.userService.AddMoneyToWallet(this.User.GetId(), model.Money);

                this.TempData[SuccessMessage] = "The money was successfully added to the wallet!";
                return this.RedirectToAction("Profile", "User");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add money in wallet! Please try again later or contact administrator!");

                return this.View(model);
            }
        }
    }
}
