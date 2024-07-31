﻿using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.Application.GenreOperations.Queries.GetGenres;

public class GetGenresQuery
{
    public readonly BookStoreDBContext _context;

    public readonly IMapper _mapper;
    public GetGenresQuery(BookStoreDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<GenresViewModel> Handle()
    {
        var genres=_context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id).ToList();
        List<GenresViewModel> returnObj = _mapper.Map <List<GenresViewModel>>(genres);
        return returnObj;
    }


}

public class GenresViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}