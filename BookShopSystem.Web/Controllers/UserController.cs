using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.Infrastructure.Extensions;
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
    }
}
