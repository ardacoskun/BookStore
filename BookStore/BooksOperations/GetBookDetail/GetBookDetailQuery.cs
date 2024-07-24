using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.BooksOperations.GetBookDetail;

public class GetBookDetailQuery
{
    public int BookId { get; set; }

    private readonly BookStoreDBContext _dbContext;
    private readonly IMapper mapper;

    public GetBookDetailQuery(BookStoreDBContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        this.mapper = mapper;
    }

    public BookDetailViewModel Handle()
    {
        var book = _dbContext.Books.Where(x => x.Id == BookId).SingleOrDefault();

        if (book is null)
        {
            throw new InvalidOperationException("There is no book with this book id.");
        }

        //BookDetailViewModel viewModel = new BookDetailViewModel();
        //viewModel.Title = book.Title;
        //viewModel.Genre = ((GenreEnum)book.GenreId).ToString();
        //viewModel.PageCount = book.PageCount;
        //viewModel.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy");

        BookDetailViewModel viewModel = mapper.Map<BookDetailViewModel>(book);

        return viewModel;

    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }

    }

}
