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
                return this.RedirectToAction("Details", "Book", new { id = houseId });
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

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool bookExists = await this.bookService
                .ExistsByIdAsync(id);
            if (!bookExists)
            {
                this.TempData[ErrorMessage] = "Book with the provided id does not exist!";

                return this.RedirectToAction("All", "Book");
            }

            bool isUserManager = await this.managerService
                .ManagerExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserManager)
            {
                this.TempData[ErrorMessage] = "You must become an manager in order to edit book info!";

                return this.RedirectToAction("Become", "Manger");
            }

            string? managerId =
                await this.managerService.GetManagerIdByUserIdAsync(this.User.GetId()!);
            bool isManagerSaller = await this.bookService
                .IsManagerWithIdSallerOfBookWithIdAsync(id, managerId!);
            if (!isManagerSaller)
            {
                this.TempData[ErrorMessage] = "You must be the manager saller of the book you want to edit!";

                return this.RedirectToAction("Mine", "Book");
            }

            try
            {
                BookPreDeleteDetailsViewModel viewModel =
                    await this.bookService.GetBookForDeleteByIdAsync(id);

                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id, BookPreDeleteDetailsViewModel model)
        {
            bool bookExists = await this.bookService
                .ExistsByIdAsync(id);
            if (!bookExists)
            {
                this.TempData[ErrorMessage] = "Book with the provided id does not exist!";

                return this.RedirectToAction("All", "Book");
            }

            bool isUserManager = await this.managerService
                .ManagerExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserManager)
            {
                this.TempData[ErrorMessage] = "You must become an manager in order to edit book info!";

                return this.RedirectToAction("Become", "Manager");
            }

            string? managerId =
                await this.managerService.GetManagerIdByUserIdAsync(this.User.GetId()!);
            bool isManagerSaller = await this.bookService
                .IsManagerWithIdSallerOfBookWithIdAsync(id, managerId!);
            if (!isManagerSaller)
            {
                this.TempData[ErrorMessage] = "You must be the manager saller of the book you want to edit!";

                return this.RedirectToAction("Mine", "Book");
            }

            try
            {
                await this.bookService.DeleteHouseByIdAsync(id);

                this.TempData[WarningMessage] = "The book was successfully deleted!";
                return this.RedirectToAction("Mine", "Book");
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool houseExists = await this.bookService
                .ExistsByIdAsync(id);
            if (!houseExists)
            {
                this.TempData[ErrorMessage] = "Book with the provided id does not exist!";

                return this.RedirectToAction("All", "Book");
            }

            bool isUserManager = await this.managerService
                .ManagerExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserManager)
            {
                this.TempData[ErrorMessage] = "You must become an manager in order to edit book info!";

                return this.RedirectToAction("Become", "Manager");
            }

            string? managerId =
                await this.managerService.GetManagerIdByUserIdAsync(this.User.GetId()!);
            bool isManagerSaller = await this.bookService
                .IsManagerWithIdSallerOfBookWithIdAsync(id, managerId!);
            if (!isManagerSaller)
            {
                this.TempData[ErrorMessage] = "You must be the manager saller of the book you want to edit!";

                return this.RedirectToAction("Mine", "Book");
            }

            try
            {
                BookFormModel formModel = await this.bookService
                    .GetBookForEditByIdAsync(id);
                formModel.Genries = await this.genreService.AllGenriesAsync();

                return this.View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, BookFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Genries = await this.genreService.AllGenriesAsync();

                return this.View(model);
            }

            bool bookExists = await this.bookService
                .ExistsByIdAsync(id);
            if (!bookExists)
            {
                this.TempData[ErrorMessage] = "Book with the provided id does not exist!";

                return this.RedirectToAction("All", "Book");
            }

            bool isUserManager = await this.managerService
                .ManagerExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserManager)
            {
                this.TempData[ErrorMessage] = "You must become an manager in order to edit book info!";

                return this.RedirectToAction("Become", "Manager");
            }

            string? managerId =
                await this.managerService.GetManagerIdByUserIdAsync(this.User.GetId()!);
            bool isManagerSaller = await this.bookService
                .IsManagerWithIdSallerOfBookWithIdAsync(id, managerId!);
            if (!isManagerSaller)
            {
                this.TempData[ErrorMessage] = "You must be the manager saller of the book you want to edit!";

                return this.RedirectToAction("Mine", "Book");
            }

            try
            {
                await this.bookService.EditBookByIdAndFormModelAsync(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to update the book. Please try again later or contact administrator!");
                model.Genries = await this.genreService.AllGenriesAsync();

                return this.View(model);
            }

            this.TempData[SuccessMessage] = "Book was edited successfully!";
            return this.RedirectToAction("Details", "Book", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> MyLibrary()
        {
            List<BookAllViewModel> myBooks =
                new List<BookAllViewModel>();

            string userId = this.User.GetId()!;
            bool isUserManager = await this.managerService
                .ManagerExistsByUserIdAsync(userId);

            try
            {
                if (isUserManager)
                {
                    string? managerId =
                        await this.managerService.GetManagerIdByUserIdAsync(userId);

                    myBooks.AddRange(await this.bookService.AllByManagerIdAsync(managerId!));
                }
                else
                {
                    myBooks.AddRange(await this.bookService.AllByUserIdAsync(userId));
                }

                return this.View(myBooks);
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
