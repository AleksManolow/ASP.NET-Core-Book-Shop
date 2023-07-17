using BookShopSystem.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShopSystem.Web.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        public BookController(IBookService bookService)
        {
            this.bookService = bookService;         
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

        }
    }
}
