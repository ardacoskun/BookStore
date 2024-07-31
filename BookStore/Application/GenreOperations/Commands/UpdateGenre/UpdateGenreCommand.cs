using BookStore.DBOperations;

namespace BookStore.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommand
{
    public int GenreId { get; set; }
    public UpdateGenreModel Model { get; set; }
    public readonly BookStoreDBContext _context;
    public UpdateGenreCommand(BookStoreDBContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var genre=_context.Genres.SingleOrDefault(x => x.Id == GenreId);

        if (genre == null)
            throw new InvalidOperationException("Genre is not found.");

        if(_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
            throw new InvalidOperationException("Genre with this name already exists.");

        genre.Name=Model.Name.Trim() == default ? Model.Name :genre.Name;
        genre.IsActive = Model.IsActive;
        _context.SaveChanges();

    }
}

public class UpdateGenreModel
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}