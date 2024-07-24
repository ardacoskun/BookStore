
using AutoMapper;
using static BookStore.BooksOperations.CreateBook.CreateBookCommand;
using static BookStore.BooksOperations.GetBookDetail.GetBookDetailQuery;
using static BookStore.BooksOperations.GetBooks.GetBooksQuery;


namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>().ReverseMap();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())).ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyy"))).ReverseMap();
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())).ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyy"))).ReverseMap();
        }
    }
}
