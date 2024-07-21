using BookStore.DBOperations;

namespace BookStore.BooksOperations.CreateBook;

public class CreateBookCommand
{
    public CreateBookModel Model { get; set; }

    private readonly BookStoreDBContext _dbContext;

    public CreateBookCommand(BookStoreDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

        if (book is not null)
        {
            throw new InvalidOperationException("This book already exists!");
        }
        book = new Book();
        book.Title = Model.Title;
        book.PublishDate = Model.PublishDate;
        book.GenreId = Model.GenreId;
        book.PageCount = Model.PageCount;

        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();

    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }
    }
}
