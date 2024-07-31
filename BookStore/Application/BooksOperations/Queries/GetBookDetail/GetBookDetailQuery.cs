﻿using AutoMapper;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.BooksOperations.Queries.GetBookDetail;

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
        var book = _dbContext.Books.Include(x => x.Genre).Where(x => x.Id == BookId).SingleOrDefault();

        if (book is null)
        {
            throw new InvalidOperationException("There is no book with this book id.");
        }

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
