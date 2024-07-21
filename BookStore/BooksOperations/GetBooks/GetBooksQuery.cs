using BookStore.Common;
using BookStore.DBOperations;

namespace BookStore.BooksOperations.GetBooks;

public class GetBooksQuery

{
    private readonly BookStoreDBContext _dbContext;
    public GetBooksQuery(BookStoreDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    public List<BooksViewModel> Handle()
    {
        var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList();
        List<BooksViewModel> books = new List<BooksViewModel>();
        foreach (var book in bookList)
        {
            books.Add(
                new BooksViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
                    PageCount = book.PageCount,
                }

                );
        }

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