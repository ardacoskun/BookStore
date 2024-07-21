using BookStore.Common;
using BookStore.DBOperations;

namespace BookStore.BooksOperations.GetBookDetail;

public class GetBookDetailQuery
{
    public int BookId { get; set; }

    private readonly BookStoreDBContext _dbContext;
    public GetBookDetailQuery(BookStoreDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public BookDetailViewModel Handle()
    {
        var book = _dbContext.Books.Where(x => x.Id == BookId).SingleOrDefault();

        if (book is null)
        {
            throw new InvalidOperationException("There is no book with this book id.");
        }

        BookDetailViewModel viewModel = new BookDetailViewModel();
        viewModel.Title = book.Title;
        viewModel.Genre = ((GenreEnum)book.GenreId).ToString();
        viewModel.PageCount = book.PageCount;
        viewModel.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy");

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
