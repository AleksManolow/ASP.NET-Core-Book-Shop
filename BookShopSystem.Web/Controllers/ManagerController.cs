using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.Infrastructure.Extensions;
using BookShopSystem.Web.ViewModels.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static BookShopSystem.Common.NotificationMessagesConstants;

namespace BookShopSystem.Web.Controllers
{
    [Authorize]
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
                this.TempData[ErrorMessage] = "You are already a manager!";

                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }
        [HttpPost]
        public async Task<IActionResult> Become(BecomeManagerFormModel model)
        {
            string? userId = this.User.GetId();
            bool isManager = await this.managerService.ManagerExistsByPhoneNumberAsync(userId);
            if (isManager)
            {
                this.TempData[ErrorMessage] = "You are already an manager!";

                return this.RedirectToAction("Index", "Home");
            }

            bool isPhoneNumberTaken =
                await this.managerService.ManagerExistsByPhoneNumberAsync(model.PhoneNumber);
            if (isPhoneNumberTaken)
            {
                this.ModelState.AddModelError(nameof(model.PhoneNumber), "Manager with the provided phone number already exists!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            bool userHasActivePurchases = await this.managerService
                .HasBooksByUserIdAsync(userId);
            if (userHasActivePurchases)
            {
                this.TempData[ErrorMessage] = "You don't have to have books to become a manager!";

                return this.RedirectToAction("Mine", "Book");
            }

            try
            {
                await this.managerService.Create(userId, model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] =
                    "Unexpected error occurred while registering you as a manager! Please try manager later or contact administrator.";

                return this.RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction("All", "Book");
        }

    }
}
