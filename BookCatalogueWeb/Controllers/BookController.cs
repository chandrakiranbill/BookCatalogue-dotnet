using Microsoft.AspNetCore.Mvc;
using BookCatalogueWeb.Models;
using BookCatalogueWeb.Services;
using System;
using System.Linq;

namespace BookCatalogueWeb.Controllers
{
    public class BookController : Controller
    {
        // ❗ STATIC so the books stay in memory between requests
        private static BookCatalog _catalog = new();

        public IActionResult Index()
        {
            Console.WriteLine("📘 Index called");

            if (!_catalog.GetAllBooks().Any())
            {
                _catalog.AddBook(new Book {
                    Title = "Clean Code",
                    Author = "Robert C. Martin",
                    Genre = "Programming",
                    Year = 2008
                });


            }

            var books = _catalog.GetAllBooks();
            Console.WriteLine($"📚 Book count: {books.Count}");

            return View(books);
        }

        // GET: /Book/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Book/Create
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _catalog.AddBook(book);
                TempData["success"] = "Book added successfully.";
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // POST: /Book/Remove
        [HttpPost]
        public IActionResult Remove(string title)
        {
            var book = _catalog.GetAllBooks().FirstOrDefault(b => b.Title == title);
            if (book != null)
            {
                _catalog.RemoveBook(book);
                TempData["success"] = $"Removed '{title}' from the catalog.";
            }
            return RedirectToAction("Index");
        }

        // GET: /Book/Search
        public IActionResult Search(string keyword, string field)
        {
             IEnumerable<Book> results = new List<Book>();

    if (!string.IsNullOrWhiteSpace(keyword))
    {
        var allBooks = _catalog.GetAllBooks();

        switch (field?.ToLower())
        {
            case "title":
                results = allBooks.Where(b => b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase));
                break;
            case "author":
                results = allBooks.Where(b => b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase));
                break;
            case "genre":
                results = allBooks.Where(b => b.Genre.Contains(keyword, StringComparison.OrdinalIgnoreCase));
                break;
            default:
                results = allBooks.Where(b =>
                    b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    b.Genre.Contains(keyword, StringComparison.OrdinalIgnoreCase));
                break;
        }
    }

    ViewBag.Keyword = keyword;
    ViewBag.Field = field;
    return View(results.ToList());
        }

        // GET: /Book/ReportByGenre
        public IActionResult ReportByGenre()
        {
            var groups = _catalog.GetBooksGroupedByGenre();
            ViewBag.GroupTitle = "Books Grouped by Genre";
            return View("ReportGrouped", groups);
        }

        // GET: /Book/ReportByAuthor
        public IActionResult ReportByAuthor()
        {
            var groups = _catalog.GetBooksGroupedByAuthor();
            ViewBag.GroupTitle = "Books Grouped by Author";
            return View("ReportGrouped", groups);
        }
    }
}
