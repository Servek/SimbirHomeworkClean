using AutoMapper;
using SimbirHomeworkClean.Application.DTOs.Book;
using SimbirHomeworkClean.Domain.Entities;

namespace SimbirHomeworkClean.Application.Mapping
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<Book, BookWithGanresDto>();
            CreateMap<Book, FullBookDto>();

            CreateMap<CreateBookWithGenresDto, Book>();
            CreateMap<CreateBookWithoutAuthorDto, Book>();
        }
    }
}
