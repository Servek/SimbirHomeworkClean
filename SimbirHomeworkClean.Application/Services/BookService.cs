using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SimbirHomeworkClean.Application.Contracts.Repositories;
using SimbirHomeworkClean.Application.Contracts.Services;
using SimbirHomeworkClean.Application.DTOs.Book;
using SimbirHomeworkClean.Application.Filters;
using SimbirHomeworkClean.Domain.Entities;
using SimbirHomeworkClean.Domain.Structs;

namespace SimbirHomeworkClean.Application.Services
{
    /// <summary>
    /// Сервис книг
    /// </summary>
    public class BookService : IBookService
    {
        /// <summary>
        /// Маппер
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Репозиторий авторов
        /// </summary>
        private readonly IAuthorRepository _authorRepository;

        /// <summary>
        /// Репозиторий книг
        /// </summary>
        private readonly IBookRepository _bookRepository;

        /// <summary>
        /// Репозиторий жанров
        /// </summary>
        private readonly IGenreRepository _genreRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="authorRepository">Репозиторий авторов</param>
        /// <param name="bookRepository">Репозиторий книг</param>
        /// <param name="genreRepository">Репозиторий жанров</param>
        public BookService(IMapper mapper,
                           IAuthorRepository authorRepository,
                           IBookRepository bookRepository,
                           IGenreRepository genreRepository)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<FullBookDto>> GetFilteredAsync(BooksQuery query)
        {
            var entities = await _bookRepository.GetFilteredListAsync(new BookFilter
            {
                AuthorFirstName = query.AuthorFirstName,
                AuthorLastName = query.AuthorLastName,
                AuthorMiddleName = query.AuthorMiddleName,
                GenreName = query.GenreName
            });

            return _mapper.Map<IEnumerable<FullBookDto>>(entities);
        }

        /// <inheritdoc />
        public async Task<FullBookDto> CreateAsync(CreateBookWithGenresDto dto)
        {
            var entity = _mapper.Map<Book>(dto);

            // Ищем существующего автора или добавляем нового
            entity.Author = (await _authorRepository.GetFilteredListAsync(new AuthorFilter
                                                                          {
                                                                              FullName = new FullName(entity.Author.FirstName,
                                                                                                      entity.Author.LastName,
                                                                                                      entity.Author.MiddleName)
                                                                          },
                                                                          false)).FirstOrDefault()
                         ?? await _authorRepository.AddAsync(entity.Author);

            // Ищем существующие и добавляем новые жанры
            if (entity.Genres.Count > 0)
            {
                var bookGenres = entity.Genres
                                       .GroupBy(g => g.GenreName)
                                       .Select(g => g.First())
                                       .ToArray();

                var dbGenres = await _genreRepository.GetListByGenreNamesAsync(bookGenres.Select(g2 => g2.GenreName));
                var newGenres = bookGenres.Where(g => dbGenres.All(eg => eg.GenreName != g.GenreName)).ToList();

                if (newGenres.Count > 0)
                {
                    newGenres = await _genreRepository.AddRangeAsync(newGenres);
                    dbGenres = dbGenres.Concat(newGenres).ToList();
                }

                entity.Genres = dbGenres;
            }

            // Создаем книгу
            entity = await _bookRepository.AddAsync(entity);

            return _mapper.Map<FullBookDto>(entity);
        }

        /// <inheritdoc />
        public async Task<FullBookDto> UpdateAsync(int id, CreateBookWithGenresDto dto)
        {
            var entity = await _bookRepository.GetByIdWithGenresAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Сущность с идентификатором " + id + " не найдена");

            // Ищем существующего автора или добавляем нового
            var author = (await _authorRepository.GetFilteredListAsync(new AuthorFilter
                                                                       {
                                                                           FullName = new FullName(dto.Author.FirstName,
                                                                                                   dto.Author.LastName,
                                                                                                   dto.Author.MiddleName)
                                                                       },
                                                                       false)).FirstOrDefault()
                      ?? await _authorRepository.AddAsync(_mapper.Map<Author>(dto.Author));

            entity = _mapper.Map(dto, entity);
            entity.Author = author;

            // Ищем существующие и добавляем новые жанры
            if (entity.Genres.Count > 0)
            {
                var bookGenres = entity.Genres
                                       .GroupBy(g => g.GenreName)
                                       .Select(g => g.First())
                                       .ToArray();

                var dbGenres = await _genreRepository.GetListByGenreNamesAsync(bookGenres.Select(g2 => g2.GenreName));
                var newGenres = bookGenres.Where(g => dbGenres.All(eg => eg.GenreName != g.GenreName)).ToList();

                if (newGenres.Count > 0)
                    dbGenres.AddRange(await _genreRepository.AddRangeAsync(newGenres));

                entity.Genres = dbGenres;
            }

            entity = await _bookRepository.UpdateAsync(entity);

            return _mapper.Map<FullBookDto>(entity);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            var entity = await _bookRepository.GetByIdWithLibraryCardsAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Сущность с идентификатором " + id + " не найдена");

            if (entity.LibraryCards.Count > 0)
                throw new ConstraintException("Книгу с идентификатором " + id + " нельзя удалить, пока она у кого-то на руках");

            await _bookRepository.DeleteAsync(entity);
        }
    }
}
