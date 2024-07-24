using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.BooksOperations.CreateBook;

public class CreateBookCommand
{
    public CreateBookModel Model { get; set; }

    private readonly BookStoreDBContext _dbContext;
    private readonly IMapper mapper;

    public CreateBookCommand(BookStoreDBContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

        if (book is not null)
        {
            throw new InvalidOperationException("This book already exists!");
        }
        //book = new Book();
        //book.Title = Model.Title;
        //book.PublishDate = Model.PublishDate;
        //book.GenreId = Model.GenreId;
        //book.PageCount = Model.PageCount;

        book = mapper.Map<Book>(Model);

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
