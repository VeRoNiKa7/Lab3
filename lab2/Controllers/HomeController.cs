using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using lab2.Models;

namespace lab2.Controllers
{
    public class HomeController : Controller
    {
        IRepository repo;
        public HomeController(IRepository r)
        {
            repo = r;
        }
        public HomeController()
        {
            repo = new BookRepository();
        }
        public ActionResult Index(int? author, string genre)
        {
            var authors = repo.GetAuthorList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            authors.Insert(0, new Author { FIO = "Все", Id = 0 });
            BooksListViewModel blvm = new BooksListViewModel
            {
                Books = repo.Choice(author, genre),
                Authors = new SelectList(authors, "Id", "FIO"),
                Genres = new SelectList(new List<string>()
            {
                "Все",
                "роман",
                "фантастика",
                "комедия",
                "детектив"
            })
            };
            return View(blvm);
        }
        //добавление авторов
        [HttpGet]
        public ActionResult Create_author()
        {
            return View("Create_author");
        }
        [HttpPost]
        public ActionResult Create_author(Author author)
        {
            repo.CreateAuthor(author);
            repo.Save();
            return RedirectToAction("Index");
        }
        //Создание новой записи
        [HttpGet]
        public ActionResult Create()
        {
            // Формируем список авторов для передачи в представление
            SelectList authors = new SelectList(repo.GetAuthorList(), "Id", "FIO");
            ViewBag.Authors = authors;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            //Добавляем книгу в таблицу
            repo.Create(book);
            repo.Save();
            // перенаправляем на главную страницу
            return RedirectToAction("Index");
        }

        //Редактирование записи
        [HttpGet]
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            // Находим в бд книгу
            Book book = repo.GetBook(id);
            if (book != null)
            {
                // Создаем список авторов для передачи в представление
                SelectList authors = new SelectList(repo.GetAuthorList(), "Id", "FIO", book.AuthorId);
                ViewBag.Authors = authors;
                return View(book);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            repo.Update(book);
            repo.Save();
            return RedirectToAction("Index");
        }

        //удаление записи
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Book b = repo.GetBook(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            Author author = repo.GetAuthor(b.AuthorId);
            ViewBag.Author = author.FIO;
            return View(b);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.Delete(id);
            repo.Save();
            return RedirectToAction("Index");
        }

        //Удаление автора
        [HttpGet]
        public ActionResult Delete_author()
        {
            // Формируем список авторов для передачи в представление
            SelectList authors = new SelectList(repo.GetAuthorList(), "Id", "FIO");
            ViewBag.Authors = authors;
            return View();
        }

        [HttpPost, ActionName("Delete_author")]
        public ActionResult DeleteConfirmed_author(Author author)
        {
            repo.DeleteAuthor(author.Id);
            repo.Save();
            return RedirectToAction("Index");
        }
        public ActionResult BookView(int id)
        {
            Book b = repo.GetBook(id);
            return View(b);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            repo.Dispose();
            base.Dispose(disposing);
        }
    }
}