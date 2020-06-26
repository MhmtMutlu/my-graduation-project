using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using bitirme.business.Abstract;
using bitirme.data.Abstract;
using bitirme.entity;
using bitirme.webui.Models;

namespace bitirme.webui.Controllers
{
    // localhost:5000/home
    public class HomeController:Controller
    {
        private IBookService _bookService;
        public HomeController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // localhost:5000/home/index
        public IActionResult Index()
        {
            var bookViewModel = new BookListViewModel()
            {
                Books = _bookService.GetHomePageBooks()
            };

            return View(bookViewModel);
        }
        // localhost:5000/home/about
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View("MyView");
        }
    }
}