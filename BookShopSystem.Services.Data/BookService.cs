using BookShopSystem.Data;
using BookShopSystem.Data.Models;
using BookShopSystem.Services.Data.Interfaces;
using BookShopSystem.Services.Data.Models.Book;
using BookShopSystem.Web.ViewModels.Book;
using BookShopSystem.Web.ViewModels.Book.Enums;
using BookShopSystem.Web.ViewModels.Home;
using BookShopSystem.Web.ViewModels.Manager;
using Microsoft.EntityFrameworkCore;

namespace BookShopSystem.Services.Data
{
    public class BookService : IBookService
    {
        private readonly BookShopDbContext dbContext;

        public BookService(BookShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AllBooksFilteredAndPagedServiceModel> AllAsync(AllBookQueryModel queryModel)
        {
            IQueryable<Book> booksQuery = this.dbContext
                .Books
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Genre))
            {
                booksQuery = booksQuery
                    .Where(b => b.Genre.Name == queryModel.Genre);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                booksQuery = booksQuery
                    .Where(h => EF.Functions.Like(h.Title, wildCard) ||
                                EF.Functions.Like(h.Author, wildCard) ||
                                EF.Functions.Like(h.Description, wildCard));
            }

            booksQuery = queryModel.BookSorting switch
            {
                BookSorting.Newest => booksQuery
                    .OrderByDescending(b => b.ReleaseDate),
                BookSorting.Oldest => booksQuery
                    .OrderBy(b => b.ReleaseDate),
                BookSorting.PriceAscending => booksQuery
                    .OrderBy(b => b.Price),
                BookSorting.PriceDescending => booksQuery
                    .OrderByDescending(h => h.Price),
                _ => booksQuery
                    .OrderByDescending(b => b.NumberOfSales)
                    .ThenBy(b => b.Title)
            };

            IEnumerable<BookAllViewModel> allBooks = await booksQuery
                .Where(b => b.IsActive)
                .Skip((queryModel.CurrentPage - 1) * queryModel.BooksPerPage)
                .Take(queryModel.BooksPerPage)
                .Select(b => new BookAllViewModel
                {
                    Id = b.Id.ToString(),
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Price = b.Price,
                    AgeRestriction = b.AgeRestriction,
                })
                .ToArrayAsync();
            int totalBooks = booksQuery.Count();

            return new AllBooksFilteredAndPagedServiceModel()
            {
                TotalBooksCount = totalBooks,
                Books = allBooks
            };

        }

        public async Task<string> CreateAndReturnIdAsync(BookFormModel model, string managerId)
        {
            Book newBook = new Book()
            {
                Title = model.Title,
                Author= model.Author,
                Description= model.Description,
                ImageUrl = model.ImageUrl,
                AgeRestriction=model.AgeRestriction,
                Price =model.Price,
                GenreId =model.GenreId,
                ManagerId = Guid.Parse(managerId)
            };

            await dbContext.Books.AddAsync(newBook);
            await dbContext.SaveChangesAsync();
            return newBook.Id.ToString();
        }

        public async Task<bool> ExistsByIdAsync(string bookId)
        {
            bool result = await this.dbContext
               .Books
               .Where(h => h.IsActive)
               .AnyAsync(h => h.Id.ToString() == bookId);

            return result;
        }

        public async Task<BookDetailsViewModel> GetDetailsByIdAsync(string bookId)
        {
            Book book = await this.dbContext
                .Books
                .Include(h => h.Genre)
                .Include(h => h.Manager)
                .ThenInclude(a => a.User)
                .Where(h => h.IsActive)
                .FirstAsync(h => h.Id.ToString() == bookId);

            return new BookDetailsViewModel
            {
                Id = book.Id.ToString(),
                Title = book.Title,
                Author = book.Author,
                ImageUrl = book.ImageUrl,
                Price = book.Price,
                NumberOfSales = book.NumberOfSales,
                Description = book.Description,
                Genre = book.Genre.Name,
                AgeRestriction = book.AgeRestriction,
                Manager = new ManagerDetailsViewModel()
                {
                    FirstName = book.Manager.FirstName,
                    LastName = book.Manager.LastName,
                    Email = book.Manager.User.Email,
                    PhoneNumber = book.Manager.PhoneNumber
                }
            };
        }

        public async Task<bool> IsBoughtByUserWithIdAsync(string bookId, string userId)
        {
            Book book = await this.dbContext
                .Books
                .FirstAsync(h => h.Id.ToString() == bookId);

            return book.Users.Any(u => u.UserId.ToString() == userId);
        }

        public async Task<IEnumerable<IndexViewModel>> TopThreeSellingBooksAsync()
        {
            IEnumerable<IndexViewModel> topTreeBooks = await this.dbContext
                .Books
                .Where(b => b.IsActive)
                .OrderByDescending(b => b.NumberOfSales)
                .ThenBy(b => b.ReleaseDate)
                .Take(3)
                .Select(b => new IndexViewModel()
                {
                    Id = b.Id.ToString(),
                    Title = b.Title,
                    ImageUrl = b.ImageUrl
                })
                .ToArrayAsync();

            return topTreeBooks;
        }
        public async Task<bool> IsManagerWithIdSellerOfBookWithIdAsync(string bookId, string managerId)
        {
            Book book = await this.dbContext
                .Books
                .Where(h => h.IsActive)
                .FirstAsync(h => h.Id.ToString() == bookId);

            return book.ManagerId.ToString() == managerId;
        }
        public async Task<BookPreDeleteDetailsViewModel> GetBookForDeleteByIdAsync(string bookId)
        {
            Book book = await this.dbContext
                .Books
                .Where(b => b.IsActive)
                .FirstAsync(b => b.Id.ToString() == bookId);

            return new BookPreDeleteDetailsViewModel
            {
                Title = book.Title,
                ImageUrl = book.ImageUrl
            };
        }
        public async Task DeleteHouseByIdAsync(string bookId)
        {
            Book bookToDelete = await this.dbContext
                .Books
                .Where(h => h.IsActive)
                .FirstAsync(h => h.Id.ToString() == bookId);

            bookToDelete.IsActive = false;

            await this.dbContext.SaveChangesAsync();
        }
        public async Task<BookFormModel> GetBookForEditByIdAsync(string bookId)
        {
            Book book = await this.dbContext
                .Books
                .Include(b => b.Genre)
                .Where(b => b.IsActive)
                .FirstAsync(b => b.Id.ToString() == bookId);

            return new BookFormModel
            {
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                ImageUrl = book.ImageUrl,
                Price = book.Price,
                GenreId = book.GenreId,
                AgeRestriction = book.AgeRestriction,
            };
        }
        public async Task EditBookByIdAndFormModelAsync(string bookId, BookFormModel formModel)
        {
            Book book = await this.dbContext
                .Books
                .Where(h => h.IsActive)
                .FirstAsync(h => h.Id.ToString() == bookId);

            book.Title = formModel.Title;
            book.Author = formModel.Author;
            book.Description = formModel.Description;
            book.ImageUrl = formModel.ImageUrl;
            book.Price = formModel.Price;
            book.GenreId = formModel.GenreId;
            book.AgeRestriction = formModel.AgeRestriction;

            await this.dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<BookAllViewModel>> AllByManagerIdAsync(string managerId)
        {
            IEnumerable<BookAllViewModel> allManagerBooks = await this.dbContext
                .Books
                .Where(b => b.IsActive &&
                            b.ManagerId.ToString() == managerId)
                .Select(b => new BookAllViewModel
                {
                    Id = b.Id.ToString(),
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Price = b.Price,
                    AgeRestriction = b.AgeRestriction,
                })
                .ToArrayAsync();

            return allManagerBooks;
        }

        public async Task<IEnumerable<BookAllViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<BookAllViewModel>allUserBooks = await this.dbContext
               .Purchases
               .Include(p => p.Book)
               .Where(p => p.UserId.ToString() == userId)
               .Select(p => new BookAllViewModel
               {
                   Id = p.Book.Id.ToString(),
                   Title = p.Book.Title,
                   Author = p.Book.Author,
                   ImageUrl = p.Book.ImageUrl,
                   Price = p.Book.Price,
                   AgeRestriction = p.Book.AgeRestriction,
               })
               .ToArrayAsync();
                
            return allUserBooks;
        }

    }
}
