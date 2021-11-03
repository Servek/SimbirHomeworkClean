using AutoMapper;
using SimbirHomeworkClean.Application.DTOs.Person;
using SimbirHomeworkClean.Domain.Entities;

namespace SimbirHomeworkClean.Application.Mapping
{
    public class PersonMappingProfile : Profile
    {
        public PersonMappingProfile()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<Person, PersonWithLibraryCardsDto>();

            CreateMap<CreatePersonDto, Person>();
        }
    }
}
