using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{

    private static List<Book> BookList = new List<Book>()
    {
        new Book{Id=1,Title="Lean Startup",GenreId=1,PageCount=200,PublishDate=new DateTime(2001,06,12)},
        new Book{Id=2,Title="Herland",GenreId=2,PageCount=250,PublishDate=new DateTime(2010,06,12)},
        new Book{Id=3,Title="Dune",GenreId=2,PageCount=540,PublishDate=new DateTime(2001,12,21)},
    };


    [HttpGet]
    public List<Book> GetBooks()
    {
        var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
        return bookList;
    }

    //FromRoute
    [HttpGet("{id}")]
    public Book GetById(int id)
    {
        var book = BookList.Where(x => id == x.Id).SingleOrDefault();
        return book;

    }

    //İki tane HttpGet olduğunda hata verir.
    //FromQuery
    //[HttpGet]
    //public Book GetByIdFromQuery([FromQuery] string id)
    //{
    //    int convertedId = Convert.ToInt32(id);
    //    var book = BookList.Find(x => x.Id == convertedId);
    //    return book;
    //}

    [HttpPost]
    public IActionResult AddBook([FromBody] Book newBook)
    {
        var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);

        if(book is not null)
        {
            return BadRequest();
        }
        BookList.Add(newBook);

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id,[FromBody] Book updatedBook)
    {
        var book = BookList.SingleOrDefault(x => x.Id == id);

        if(book is null)
        {
            return BadRequest("No such book found!");
        }
        book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

        return Ok(updatedBook);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = BookList.Find(x => x.Id == id);
        if(book is null)
        {
            return BadRequest("No registered book found!");
        }

        BookList.Remove(book);
        return Ok($"{book.Title} successfully deleted.");
    }



}
