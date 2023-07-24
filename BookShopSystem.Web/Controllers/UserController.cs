using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

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
            /*if ()
            {

            }*/
            return View(await userService.GetUserProfileInfo(this.User.GetId()));  
        }
    }
}
