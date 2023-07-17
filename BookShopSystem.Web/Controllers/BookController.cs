using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.Infrastructure.Extensions;
using BookShopSystem.Web.ViewModels.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static BookShopSystem.Common.NotificationMessagesConstants;

namespace BookShopSystem.Web.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly IGenreService genreService;
        private readonly IManagerService managerService;
        public BookController(IBookService bookService, IGenreService genreService, IManagerService managerService)
        {
            this.bookService = bookService;  
            this.genreService = genreService;
            this.managerService = managerService;   
        }

        /*[HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Books()
        {

            return View();
        }*/
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isManager = await this.managerService.ManagerExistsByUserIdAsync(this.User.GetId());
            if (!isManager)
            {
                this.TempData[ErrorMessage] = "You must become an manager in order to add new books!";
                return this.RedirectToAction("Become", "Manager");
            }

            BookFormModel formModel = new BookFormModel()
            {
                Genries = await genreService.AllGenriesAsync()
            };
            return View(formModel);
        }
    }
}
