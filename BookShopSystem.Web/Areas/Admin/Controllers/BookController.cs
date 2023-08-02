using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Web.Areas.Admin.ViewModels.Book;
using BookShopSystem.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BookShopSystem.Web.Areas.Admin.Controllers
{
    public class BookController : BaseAdminController
    {
        private readonly IBookService bookService;
        private readonly IManagerService managerService;
        public BookController(IBookService bookService, IManagerService managerService)
        {
            this.bookService = bookService;
            this.managerService = managerService;
        }
        public async Task<IActionResult> Mine()
        {
            string managetId =
                await this.managerService.GetManagerIdByUserIdAsync(this.User.GetId());
            MyBooksViewModel viewModel = new MyBooksViewModel()
            {
                AddedBooks = await bookService.AllByManagerIdAsync(managetId),
                BoughtBooks = await bookService.AllByUserIdAsync(this.User.GetId())
            };

            return View(viewModel);
        }
    }
}
