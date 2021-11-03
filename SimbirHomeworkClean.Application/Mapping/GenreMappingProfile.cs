using AutoMapper;
using SimbirHomeworkClean.Application.DTOs.Genre;
using SimbirHomeworkClean.Domain.Entities;

namespace SimbirHomeworkClean.Application.Mapping
{
    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile()
        {
            CreateMap<Genre, GenreDto>();

            CreateMap<CreateGenreDto, Genre>();
        }
    }
}
