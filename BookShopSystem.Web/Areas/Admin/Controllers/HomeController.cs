using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.Areas.Admin.ViewModels.Book;
using BookShopSystem.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BookShopSystem.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
