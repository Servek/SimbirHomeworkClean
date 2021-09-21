using AutoMapper;
using SimbirHomeworkClean.Application.DTOs.Author;

namespace SimbirHomeworkClean.Application.Mapping
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<Domain.Entities.Author, AuthorDto>();
            CreateMap<Domain.Entities.Author, FullAuthorDto>();

            CreateMap<CreateAuthorDto, Domain.Entities.Author>();
            CreateMap<CreateAuthorWithBooksDto, Domain.Entities.Author>();
        }
    }
}
