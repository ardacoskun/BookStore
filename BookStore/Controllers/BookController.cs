using BookStore.BooksOperations.CreateBook;
using BookStore.BooksOperations.GetBooks;
using BookStore.DBOperations;
using Microsoft.AspNetCore.Mvc;
using static BookStore.BooksOperations.CreateBook.CreateBookCommand;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{

    private readonly BookStoreDBContext _context;

    public BookController(BookStoreDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        //var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
        //return bookList;

        GetBooksQuery query = new GetBooksQuery(_context);
        var result = query.Handle();
        return Ok(result);
    }

    //FromRoute
    [HttpGet("{id}")]
    public Book GetById(int id)
    {
        var book = _context.Books.Where(x => id == x.Id).SingleOrDefault();
        return book;

    }

    //İki tane HttpGet olduğunda hata verir.
    //FromQuery
    //[HttpGet]
    //public Book GetByIdFromQuery([FromQuery] string id)
    //{
    //    int convertedId = Convert.ToInt32(id);
    //    var book = _context.Books.Find(x => x.Id == convertedId);
    //    return book;
    //}

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        //var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);

        //if (book is not null)
        //{
        //    return BadRequest();
        //}
        //_context.Books.Add(newBook);
        //_context.SaveChanges();

        CreateBookCommand command = new CreateBookCommand(_context);

        try
        {
            command.Model = newBook;
            command.Handle();

            return Ok($"{newBook.Title} has saved successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = _context.Books.SingleOrDefault(x => x.Id == id);

        if (book is null)
        {
            return BadRequest("No such book found!");
        }
        book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

        _context.SaveChanges();

        return Ok(updatedBook);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = _context.Books.SingleOrDefault(x => x.Id == id);
        if (book is null)
        {
            return BadRequest("No registered book found!");
        }

        _context.Books.Remove(book);
        _context.SaveChanges();

        return Ok($"{book.Title} successfully deleted.");
    }



}
