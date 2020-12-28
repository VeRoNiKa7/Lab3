using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab2.Models
{
    public class Book
    {
        // ID книги
        public int BookId { get; set; }
        // название книги
        public string Name { get; set; }
        // жанр книги
        public string Genre { get; set; }
        //описание
        public string Description { get; set; }
       
        // ID автора
        public int? AuthorId { get; set; }
        public Author Author { get; set; }
    }
}