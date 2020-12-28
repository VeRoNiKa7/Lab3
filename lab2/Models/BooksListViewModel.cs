using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace lab2.Models
{
    public class BooksListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public SelectList Authors { get; set; }
        public SelectList Genres { get; set; }
    }
}