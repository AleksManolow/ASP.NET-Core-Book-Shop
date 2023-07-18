using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Services.Data.Models.Book;
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery]AllBookQueryModel queryModel)
        {
            AllBooksFilteredAndPagedServiceModel serviceModel = await this.bookService.AllAsync(queryModel);

            queryModel.Books = serviceModel.Books;
            queryModel.TotalBooks = serviceModel.TotalBooksCount;
            queryModel.Genries = await this.genreService.AllGenriesNamesAsync();

            return this.View(queryModel);
        }
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
        [HttpPost]
        public async Task<IActionResult> Add(BookFormModel model)
        {
            bool isManager =
                await this.managerService.ManagerExistsByUserIdAsync(this.User.GetId()!);
            if (!isManager)
            {
                this.TempData[ErrorMessage] = "You must become an manager in order to add new book!";

                return this.RedirectToAction("Become", "Manager");
            }

            bool categoryExists =
                await this.genreService.ExistsByIdAsync(model.GenreId);
            if (!categoryExists)
            {
                // Adding model error to ModelState automatically makes ModelState Invalid
                this.ModelState.AddModelError(nameof(model.GenreId), "Selected genre does not exist!");
            }

            if (!this.ModelState.IsValid)
            {
                model.Genries = await this.genreService.AllGenriesAsync();

                return this.View(model);
            }

            try
            {
                string? managerId =
                    await this.managerService.GetManagerIdByUserIdAsync(this.User.GetId()!);

                string houseId =
                    await this.bookService.CreateAndReturnIdAsync(model, managerId);

                this.TempData[SuccessMessage] = "Book was added successfully!";
                return this.RedirectToAction("Details", "House", new { id = houseId });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new book! Please try again later or contact administrator!");
                model.Genries = await this.genreService.AllGenriesAsync();

                return this.View(model);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool bookExists = await this.bookService
                .ExistsByIdAsync(id);
            if (!bookExists)
            {
                this.TempData[ErrorMessage] = "Book with the provided id does not exist!";

                return this.RedirectToAction("All", "Book");
            }

            try
            {
                BookDetailsViewModel viewModel = await this.bookService
                    .GetDetailsByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
