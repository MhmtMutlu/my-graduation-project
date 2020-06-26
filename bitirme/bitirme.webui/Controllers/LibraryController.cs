using System.Linq;
using Microsoft.AspNetCore.Mvc;
using bitirme.business.Abstract;
using bitirme.entity;
using bitirme.webui.Models;

namespace bitirme.webui.Controllers
{
    public class LibraryController:Controller  
    {
        private IBookService _bookService;
        public LibraryController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult List(string category, int page = 1) 
        {
            const int pageSize = 6;
            var bookViewModel = new BookListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _bookService.GetCountByCategory(category),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    CurrentCategory = category
                },
                Books = _bookService.GetBooksByCategory(category, page, pageSize)
            };

            return View(bookViewModel);
        }

        public IActionResult Details(string url)
        {
            if (url == null)
            {
                return NotFound();
            }
            Book book = _bookService.GetBookDetails(url);

            if (book == null)
            {
                return NotFound();
            }
            return View(new BookDetailModel{
                Book = book,
                Categories = book.BookCategories.Select(s => s.Category).ToList()
            });
        }
        public IActionResult Search(string q)
        {
            var bookViewModel = new BookListViewModel()
            {
                Books = _bookService.GetSearchResult(q)
            };

            return View(bookViewModel);
        }
    }
}