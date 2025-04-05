using System;

namespace BookCatalogingSystem
{
    public class Book
    {
        public string Title { get; }
        public string Author { get; }
        public string Genre { get; }
        public int Year { get; }

        public Book(string title, string author, string genre, int year)
        {
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
        }

        public override string ToString()
        {
            return $"{Title} by {Author}, Genre: {Genre}, Year: {Year}";
        }
    }
}