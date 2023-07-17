using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

using static BookShopSystem.Common.NotificationMessagesConstants;

namespace BookShopSystem.Web.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IManagerService managerService;

        public ManagerController(IManagerService managerService)
        {
            this.managerService = managerService;
        }
        [HttpGet]
        public async Task<IActionResult> Become() 
        {
            string userId = this.User.GetId();
            bool isAgent = await this.managerService.ManagerExistsByUserIdAsync(userId);

            if (isAgent)
            {
                this.TempData[ErrorMessage] = "You are already an manager!";

                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

    }
}
