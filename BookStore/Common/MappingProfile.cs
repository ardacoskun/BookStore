
using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.Entities;
using static BookStore.Application.BooksOperations.Commands.CreateBook.CreateBookCommand;
using static BookStore.Application.BooksOperations.Queries.GetBookDetail.GetBookDetailQuery;
using static BookStore.Application.BooksOperations.Queries.GetBooks.GetBooksQuery;


namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>().ReverseMap();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())).ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyy"))).ReverseMap();
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())).ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyy"))).ReverseMap();
            CreateMap<Genre, GenresViewModel>().ReverseMap();
        }
    }
}
