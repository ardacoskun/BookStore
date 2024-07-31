using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQuery
{
    public int GenreId { get; set; }
    public readonly BookStoreDBContext _context;
    public readonly IMapper _mapper;
    public GetGenreDetailQuery(BookStoreDBContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public GenreDetailViewModel Handle()
    {
        var genre=_context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);

        return genre == null ? throw new InvalidOperationException("Genre not found.") : _mapper.Map<GenreDetailViewModel>(genre);
    }
}

public class GenreDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}