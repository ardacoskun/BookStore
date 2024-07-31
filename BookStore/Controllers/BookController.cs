using AutoMapper;
using BookStore.Application.BooksOperations.Commands.CreateBook;
using BookStore.Application.BooksOperations.Commands.DeleteBook;
using BookStore.DBOperations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using static BookStore.Application.BooksOperations.Commands.CreateBook.CreateBookCommand;
using static BookStore.Application.BooksOperations.Queries.GetBookDetail.GetBookDetailQuery;
using static BookStore.Application.BooksOperations.Commands.UpdateBook.UpdateBookCommand;
using BookStore.Application.BooksOperations.Commands.UpdateBook;
using BookStore.Application.BooksOperations.Queries.GetBookDetail;
using BookStore.Application.BooksOperations.Queries.GetBooks;

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
        GetBooksQuery query = new GetBooksQuery(_context, mapper);
        var result = query.Handle();
        return Ok(result);
    }

    //FromRoute
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {

            BookDetailViewModel result;
            GetBookDetailQuery query = new GetBookDetailQuery(_context, mapper);
            query.BookId = id;

            GetBookDetailQueryValidator validator = new();
            validator.ValidateAndThrow(query);

            result = query.Handle();

            return Ok(result);

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
            CreateBookCommand command = new CreateBookCommand(_context, mapper);
            command.Model = newBook;

            CreateBookCommandValidator validator = new();
            ValidationResult result= validator.Validate(command);

            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok($"{newBook.Title} has saved successfully.");

    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;

            UpdateBookCommandValidator validator= new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok($"{updatedBook.Title} updated successfully");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;

            DeleteBookCommandValidator validator=new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok("Bookd deleted successfully.");
    }



}
