using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.Areas.Admin.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace BookShopSystem.Web.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IActionResult> All()
        {
            IEnumerable<UserViewModel> viewModel = 
                await this.userService.AllAsync();

            return View(viewModel);
        }
    }
}
