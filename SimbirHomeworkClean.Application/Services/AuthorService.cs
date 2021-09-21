using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SimbirHomeworkClean.Application.Contracts.Repositories;
using SimbirHomeworkClean.Application.Contracts.Services;
using SimbirHomeworkClean.Application.DTOs.Author;
using SimbirHomeworkClean.Application.Filters;
using SimbirHomeworkClean.Domain.Entities;

namespace SimbirHomeworkClean.Application.Services
{
    /// <summary>
    /// Сервис авторов
    /// </summary>
    public class AuthorService : IAuthorService
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
        public AuthorService(IMapper mapper,
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
        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<AuthorDto>>(await _authorRepository.GetListAsync());
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AuthorDto>> GetFilteredAsync(AuthorsQuery query)
        {
            return _mapper.Map<IEnumerable<AuthorDto>>(await _authorRepository.GetFilteredListAsync(
                                                           new AuthorFilter
                                                           {
                                                               BookNameTerm = query.BookNameTerm,
                                                               BookWritingYear = query.BookWritingYear
                                                           },
                                                           query.IsDescOrder));
        }

        /// <inheritdoc />
        public async Task<FullAuthorDto> GetByIdAsync(int id)
        {
            var entity = await _authorRepository.GetByIdWithBooksAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Сущность с идентификатором " + id + " не найдена");

            return _mapper.Map<FullAuthorDto>(entity);
        }

        /// <inheritdoc />
        public async Task<FullAuthorDto> CreateAsync(CreateAuthorWithBooksDto dto)
        {
            var entity = _mapper.Map<Author>(dto);
            entity = await _authorRepository.AddAsync(entity);

            if (entity.Books.Count > 0)
            {
                var bookGenres = entity.Books
                                       .SelectMany(b => b.Genres)
                                       .GroupBy(g => g.GenreName)
                                       .Select(g => g.First())
                                       .ToArray();

                if (bookGenres.Length > 0)
                {
                    var dbGenres = await _genreRepository.GetListByGenreNamesAsync(bookGenres.Select(g2 => g2.GenreName));
                    var newGenres = bookGenres.Where(g => dbGenres.All(eg => eg.GenreName != g.GenreName)).ToList();

                    if (newGenres.Count > 0)
                    {
                        newGenres = await _genreRepository.AddRangeAsync(newGenres);
                        dbGenres = dbGenres.Concat(newGenres).ToList();
                    }

                    foreach (var book in entity.Books)
                        book.Genres = dbGenres.Where(ag => book.Genres.Select(g => g.GenreName).Contains(ag.GenreName)).ToList();
                }

                entity.Books = (await _bookRepository.AddRangeAsync(entity.Books)).ToArray();
            }

            return _mapper.Map<FullAuthorDto>(entity);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            var entity = await _authorRepository.GetByIdWithBooksAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Сущность с идентификатором " + id + " не найдена");

            if (entity.Books.Any())
                throw new ConstraintException("Автора с идентификатором " + id + " нельзя удалить, пока у него есть книги");

            await _authorRepository.DeleteAsync(entity);
        }
    }
}
