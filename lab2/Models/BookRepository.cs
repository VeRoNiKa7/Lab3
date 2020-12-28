using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace lab2.Models
{
    public interface IRepository : IDisposable
    {
        List<Book> GetBookList();
        Book GetBook(int? id);
        List<Author> GetAuthorList();
        Author GetAuthor(int? id);
        List<Book> Choice(int? author, string genre);
        void Create(Book item);
        void CreateAuthor(Author item);
        void Update(Book item);
        void Delete(int id);
        void DeleteAuthor(int id);
        void Save();
    }
    public class BookRepository : IRepository
    {
        private BookContext db;
        public BookRepository()
        {
            this.db = new BookContext();
        }
        public List<Book> GetBookList()
        {
            return db.Books.ToList();
        }
        public List<Author> GetAuthorList()
        {
            return db.Authors.ToList();
        }
        public List<Book> Choice(int? author, string genre)
        {
            IQueryable<Book> books = db.Books.Include(b => b.Author);
            if (author != null && author != 0)
            {
                books = books.Where(b => b.AuthorId == author);
            }
            if (!String.IsNullOrEmpty(genre) && !genre.Equals("Все"))
            {
                books = books.Where(b => b.Genre == genre);
            }
            return books.ToList();
        }
        public Book GetBook(int? id)
        {
            return db.Books.Find(id);
        }
        public Author GetAuthor(int? id)
        {
            return db.Authors.Find(id);
        }

        public void Create(Book b)
        {
            db.Books.Add(b);
        }
        public void CreateAuthor(Author a)
        {
            db.Authors.Add(a);
        }
        public void Update(Book b)
        {
            db.Entry(b).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Book b = db.Books.Find(id);
            if (b != null)
                db.Books.Remove(b);
        }
        public void DeleteAuthor(int id)
        {
            Author a = db.Authors.Find(id);
            if (a != null)
                db.Authors.Remove(a);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}