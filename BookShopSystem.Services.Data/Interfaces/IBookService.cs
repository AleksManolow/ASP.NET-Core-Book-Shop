﻿using BookShopSystem.Web.ViewModels.Book;
using BookShopSystem.Web.ViewModels.Home;

namespace BookShopSystem.Services.Data.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<IndexViewModel>> TopThreeSellingBooksAsync();
        Task<string> CreateAndReturnIdAsync(BookFormModel model, string managerId);
    }
}
