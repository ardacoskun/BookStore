using AutoMapper;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly BookStoreDBContext _context;
    private readonly IMapper _mapper;

    public GenreController(BookStoreDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetGenres()
    {
        GetGenresQuery query = new(_context,_mapper);
        var obj = query.Handle();

        return Ok(obj);
    }

    [HttpGet("id")]
    public IActionResult GetId([FromRoute] int id)
    {
        GetGenreDetailQuery query= new(_context,_mapper);
        query.GenreId = id;

        GetGenreDetailValidator validator=new GetGenreDetailValidator();
        validator.ValidateAndThrow(query);

        var obj = query.Handle();
        return Ok(obj);
    }

    [HttpPost]
    public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
    {
        CreateGenreCommand command = new CreateGenreCommand(_context);
        command.Model = newGenre;

        CreateGenreCommandValidator validator=new CreateGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok(); 
    }

    [HttpPut("id")]
    public IActionResult UpdateGenre([FromRoute] int id, [FromBody] UpdateGenreModel updateGenreModel)
    {
        UpdateGenreCommand command=new UpdateGenreCommand(_context);
        command.GenreId=id;

        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Model = updateGenreModel;

        command.Handle();
        return Ok();
    }

}
