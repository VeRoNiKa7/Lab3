using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab2.Models
{
    public class Author
    {
        // ID автора
        public int Id { get; set; }
        // автор книги
        public string FIO { get; set; }
        public ICollection<Book> Books { get; set; }
        public Author()
        {
            Books = new List<Book>();
        }
    }
}