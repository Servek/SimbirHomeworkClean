using AutoMapper;
using SimbirHomeworkClean.Application.DTOs.LibraryCard;
using SimbirHomeworkClean.Domain.Entities;

namespace SimbirHomeworkClean.Application.Mapping
{
    public class LibraryCardMappingProfile : Profile
    {
        public LibraryCardMappingProfile()
        {
            CreateMap<LibraryCard, LibraryCardWithoutPersonDto>();
        }
    }
}
