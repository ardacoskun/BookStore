using AutoMapper;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.BooksOperations.Queries.GetBooks;

public class GetBooksQuery

{
    private readonly BookStoreDBContext _dbContext;
    private readonly IMapper mapper;

    public GetBooksQuery(BookStoreDBContext dBContext, IMapper mapper)
    {
        _dbContext = dBContext;
        this.mapper = mapper;
    }

    public List<BooksViewModel> Handle()
    {
        var bookList = _dbContext.Books.Include(x => x.Genre).OrderBy(x => x.Id).ToList();
        List<BooksViewModel> books = mapper.Map<List<BooksViewModel>>(bookList);

        return books;
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }

    }

}