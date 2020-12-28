using Microsoft.VisualStudio.TestTools.UnitTesting;
using lab2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using lab2.Models;
using System.Web;
using System.Web.Mvc;

namespace lab2.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            var mock = new Mock<IRepository>();
            var authorId = 7;
            var genre = "роман";
            mock.Setup(a => a.Choice(authorId, genre)).Returns(new List<Book>());
            mock.Setup(a => a.GetAuthorList()).Returns(new List<Author>());
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.Index(authorId, genre) as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod()]
        public void Create_authorTest()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Create_author() as ViewResult;
            Assert.AreEqual("Create_author", result.ViewName);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(a => a.GetAuthorList()).Returns(new List<Author>() { new Author() });
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.Create() as ViewResult;
            SelectList actual = result.ViewBag.Authors as SelectList;

            // Assert
            Assert.IsNotNull(actual);
        }
        public void CreateEqualCountTest()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(a => a.GetAuthorList()).Returns(new List<Author>() { new Author(), new Author(), new Author(), new Author() });
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.Create() as ViewResult;
            SelectList actual = result.ViewBag.Authors as SelectList;

            // Assert
            Assert.AreEqual(actual.Count(), 4);
        }

        [TestMethod()]
        public void CreateBookEqualTest()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(a => a.GetAuthorList()).Returns(new List<Author>() { new Author() { Id = 1, FIO = "Л.Н. Толстой" } });
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.Create() as ViewResult;
            SelectList actual = result.ViewBag.Authors as SelectList;

            // Assert
            Assert.AreEqual("FIO", actual.DataTextField);
        }

        [TestMethod()]
        public void EditBookTest()
        {
            var mock = new Mock<IRepository>();
            var id = 3;
            mock.Setup(a => a.GetBook(id)).Returns(new Book());
            mock.Setup(a => a.GetAuthorList()).Returns(new List<Author>());
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.EditBook(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod()]
        public void EditBook_AuthorListTest()
        {
            var mock = new Mock<IRepository>();
            var id = 2;
            mock.Setup(a => a.GetBook(id)).Returns(new Book());
            mock.Setup(a => a.GetAuthorList()).Returns(new List<Author>());
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.EditBook(id) as ViewResult;
            SelectList actual = result.ViewBag.Authors as SelectList;
            // Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var mock = new Mock<IRepository>();
            var id = 4;
            mock.Setup(a => a.GetBook(id)).Returns(new Book()
            {
                BookId = id,
                Name = "Властелин колец",
                Genre = "фантастика",
                Description = "гномы, эльфы, гоблины, а еще агроном какой-то",
                AuthorId = id
            });
            mock.Setup(a => a.GetAuthor(id)).Returns(new Author());
            HomeController controller = new HomeController(mock.Object);
            // Act
            ViewResult result = controller.Delete(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod()]
        public void Delete_EqualAuthorTest()
        {
            var mock = new Mock<IRepository>();
            var id = 4;
            string fio = "Толкин";
            mock.Setup(a => a.GetBook(id)).Returns(new Book()
            {
                BookId = id,
                Name = "Властелин колец",
                Genre = "фантастика",
                Description = "гномы, эльфы, гоблины, а еще агроном какой-то",
                AuthorId = id
            });
            mock.Setup(a => a.GetAuthor(id)).Returns(new Author()
            {
                FIO = "Толкин"
            });
            HomeController controller = new HomeController(mock.Object);
            // Act
            ViewResult result = controller.Delete(id) as ViewResult;
            string actual = result.ViewBag.Author as string;

            // Assert
            Assert.AreEqual(fio, actual);
        }

        [TestMethod()]
        public void Delete_authorTest()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(a => a.GetAuthorList()).Returns(new List<Author>() { new Author(), new Author() });
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.Delete_author() as ViewResult;
            SelectList actual = result.ViewBag.Authors as SelectList;

            // Assert
            Assert.AreEqual(actual.Count(), 2);
        }

        [TestMethod()]
        public void BookViewTest()
        {
            var mock = new Mock<IRepository>();
            var id = 4;
            mock.Setup(a => a.GetBook(id)).Returns(new Book());
            HomeController controller = new HomeController(mock.Object);
            // Act
            ViewResult result = controller.BookView(id) as ViewResult;
            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod()]
        public void AboutTest()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.About() as ViewResult;
            string actual = result.ViewBag.Message as string;
            Assert.AreEqual("Your application description page.", actual);
        }

        [TestMethod()]
        public void ContactTest()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Contact() as ViewResult;
            string actual = result.ViewBag.Message as string;
            Assert.AreEqual("Your contact page.", actual);
        }
    }
}