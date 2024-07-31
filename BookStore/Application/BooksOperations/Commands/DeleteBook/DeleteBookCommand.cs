using BookStore.DBOperations;

namespace BookStore.Application.BooksOperations.Commands.DeleteBook;

public class DeleteBookCommand
{
    public int BookId { get; set; }

    private readonly BookStoreDBContext _dbContext;
    public DeleteBookCommand(BookStoreDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    public void Handle()
    {
        var book = _dbContext.Books.FirstOrDefault(x => x.Id == BookId);

        if (book is null)
        {
            throw new InvalidOperationException("There is no book with this book id.");
        }

        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();
    }
}
