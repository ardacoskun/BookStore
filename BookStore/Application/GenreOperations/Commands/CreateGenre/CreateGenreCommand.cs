using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommand
{
    public CreateGenreModel Model { get; set; }
    public readonly BookStoreDBContext _context;
    public CreateGenreCommand(BookStoreDBContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);

        if (genre == null)
            throw new InvalidOperationException("Genre is already exists.");

        genre = new Genre();
        genre.Name = Model.Name;

        _context.Genres.Add(genre);
        _context.SaveChanges();

    }
}

public class CreateGenreModel
{
    public string Name { get; set; }
}
