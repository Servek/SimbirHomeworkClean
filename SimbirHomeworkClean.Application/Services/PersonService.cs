using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SimbirHomeworkClean.Application.Contracts.Repositories;
using SimbirHomeworkClean.Application.Contracts.Services;
using SimbirHomeworkClean.Application.DTOs.LibraryCard;
using SimbirHomeworkClean.Application.DTOs.Person;
using SimbirHomeworkClean.Application.Filters;
using SimbirHomeworkClean.Domain.Entities;

namespace SimbirHomeworkClean.Application.Services
{
    /// <summary>
    /// Сервис людей
    /// </summary>
    public class PersonService : IPersonService
    {
        /// <summary>
        /// Маппер
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Репозиторий людей
        /// </summary>
        private readonly IPersonRepository _personRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="personRepository">Репозиторий жанров</param>
        public PersonService(IMapper mapper,
                             IPersonRepository personRepository)
        {
            _mapper = mapper;
            _personRepository = personRepository;
        }

        /// <inheritdoc />
        public async Task<PersonDto> CreateAsync(CreatePersonDto dto)
        {
            var entity = _mapper.Map<Person>(dto);
            entity = await _personRepository.AddAsync(entity);
            return _mapper.Map<PersonDto>(entity);
        }

        /// <inheritdoc />
        public async Task<PersonDto> UpdateAsync(int id, CreatePersonDto dto)
        {
            var entity = await _personRepository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Сущность с идентификатором " + id + " не найдена");

            entity = _mapper.Map(dto, entity);

            entity = await _personRepository.UpdateAsync(entity);

            return _mapper.Map<PersonDto>(entity);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            var entity = await _personRepository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Сущность с идентификатором " + id + " не найдена");

            await _personRepository.DeleteAsync(entity);
        }

        /// <inheritdoc />
        public async Task DeleteByFullNameAsync(DeletePersonByFullNameCommand command)
        {
            var entities = await _personRepository.GetFilteredListAsync(new PersonFilter
            {
                FirstName = command.FirstName, LastName = command.LastName, MiddleName = command.MiddleName
            });

            if (entities.Any())
                await _personRepository.DeleteRangeAsync(entities);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<LibraryCardWithoutPersonDto>> GetPersonLibraryCardsAsync(int id)
        {
            return _mapper.Map<IEnumerable<LibraryCardWithoutPersonDto>>(await _personRepository.GetLibraryCardsAsync(id));
        }

        /// <inheritdoc />
        public async Task<PersonWithLibraryCardsDto> ReceiveBookAsync(int id, int bookId)
        {
            var entity = await _personRepository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Сущность с идентификатором " + id + " не найдена");

            return _mapper.Map<PersonWithLibraryCardsDto>(await _personRepository.ReceiveBookAsync(entity, bookId));
        }

        /// <inheritdoc />
        public async Task<PersonWithLibraryCardsDto> ReturnBookAsync(int id, int bookId)
        {
            var entity = await _personRepository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Сущность с идентификатором " + id + " не найдена");

            return _mapper.Map<PersonWithLibraryCardsDto>(await _personRepository.ReturnBookAsync(entity, bookId));
        }
    }
}
