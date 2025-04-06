using System;

namespace BookCatalogueWeb.Models
{
    public class Book
    {

        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Genre { get; set; }
        public int Year { get; set; }



        public override string ToString()
        {
            return $"{Title} by {Author}, Genre: {Genre}, Year: {Year}";
        }
    }
}