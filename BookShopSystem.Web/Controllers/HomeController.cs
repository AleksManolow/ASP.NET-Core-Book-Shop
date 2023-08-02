using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using static BookShopSystem.Common.GeneralApplicationConstants;

namespace BookShopSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService bookService;
        public HomeController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User.IsInRole(AdminRoleName))
            {
                return this.RedirectToAction("Index", "Home", new { Area = AdminAreaName } );
            }
            IEnumerable<IndexViewModel> viewModels = await bookService.TopThreeSellingBooksAsync();
            return View(viewModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400 || statusCode == 404)
            {
                return View("Error404");
            }

            if (statusCode == 401)
            {
                return View("Error401");
            }

            return View();
        }
    }
}