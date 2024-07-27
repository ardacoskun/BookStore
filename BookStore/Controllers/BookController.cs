using AutoMapper;
using BookStore.BooksOperations.CreateBook;
using BookStore.BooksOperations.DeleteBook;
using BookStore.BooksOperations.GetBookDetail;
using BookStore.BooksOperations.GetBooks;
using BookStore.BooksOperations.UpdateBook;
using BookStore.DBOperations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using static BookStore.BooksOperations.CreateBook.CreateBookCommand;
using static BookStore.BooksOperations.GetBookDetail.GetBookDetailQuery;
using static BookStore.BooksOperations.UpdateBook.UpdateBookCommand;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{

    private readonly BookStoreDBContext _context;
    private readonly IMapper mapper;

    public BookController(BookStoreDBContext context, IMapper mapper)
    {
        _context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        //var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
        //return bookList;

        GetBooksQuery query = new GetBooksQuery(_context, mapper);
        var result = query.Handle();
        return Ok(result);
    }

    //FromRoute
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        //var book = _context.Books.Where(x => id == x.Id).SingleOrDefault();
        //return book;

        BookDetailViewModel result;

        //try
        //{
            GetBookDetailQuery query = new GetBookDetailQuery(_context, mapper);
            query.BookId = id;

            GetBookDetailQueryValidator validator = new();
            validator.ValidateAndThrow(query);

            result = query.Handle();

            return Ok(result);
        //}
        //catch (Exception ex)
        //{
        //    return BadRequest(ex.Message);
        //}


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

        CreateBookCommand command = new CreateBookCommand(_context, mapper);

        //try
        //{
            command.Model = newBook;

            CreateBookCommandValidator validator = new();
            ValidationResult result= validator.Validate(command);

            //if(!result.IsValid)
            //foreach (var item in result.Errors)
            //Console.WriteLine("Özellik:" + item.PropertyName + "-Error Message:" +item.ErrorMessage);
            //else
            //{
            //command.Handle();
            //}

            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok($"{newBook.Title} has saved successfully.");


        //}
        //catch (Exception ex)
        //{
        //    return BadRequest(ex.Message);
        //}

    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
        //var book = _context.Books.SingleOrDefault(x => x.Id == id);

        //if (book is null)
        //{
        //    return BadRequest("No such book found!");
        //}
        //book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        //book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        //book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        //book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

        //_context.SaveChanges();

        //return Ok(updatedBook);

        //try
        //{
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;

            UpdateBookCommandValidator validator= new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok($"{updatedBook.Title} updated successfully");
        //}
        //catch (Exception ex)
        //{
        //    return BadRequest(ex.Message);
        //}
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        //var book = _context.Books.SingleOrDefault(x => x.Id == id);
        //if (book is null)
        //{
        //    return BadRequest("No registered book found!");
        //}

        //_context.Books.Remove(book);
        //_context.SaveChanges();

        //try
        //{
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;

            DeleteBookCommandValidator validator=new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok("Bookd deleted successfully.");
        //}
        //catch (Exception ex)
        //{
        //    return BadRequest(ex.Message);
        //}

    }



}
