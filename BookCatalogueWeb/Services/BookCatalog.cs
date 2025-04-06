using System;
using System.Collections.Generic;
using System.Linq;

using BookCatalogueWeb.Models;

namespace BookCatalogueWeb.Services
{
    public class BookCatalog
    {
        private List<Book> books;

        public BookCatalog()
        {
            books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            if (book != null)
            {
                books.Add(book);
            }
        }

        public void RemoveBook(Book book)
        {
            if (book != null && books.Contains(book))
            {
                books.Remove(book);
            }
        }

        public List<Book> SearchBooks(string keyword)
        {
            return books.Where(b =>
                b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                b.Genre.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public List<IGrouping<string, Book>> GetBooksGroupedByGenre()
        {
            return books.GroupBy(b => b.Genre).ToList();
        }

        public List<IGrouping<string, Book>> GetBooksGroupedByAuthor()
        {
            return books.GroupBy(b => b.Author).ToList();
        }
    }

}
