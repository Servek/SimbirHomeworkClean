using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SimbirHomeworkClean.Application.Contracts.Repositories;
using SimbirHomeworkClean.Application.Contracts.Services;
using SimbirHomeworkClean.Application.DTOs.Genre;
using SimbirHomeworkClean.Domain.Entities;

namespace SimbirHomeworkClean.Application.Services
{
    /// <summary>
    /// Сервис жанров
    /// </summary>
    public class GenreService : IGenreService
    {
        /// <summary>
        /// Маппер
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Репозиторий жанров
        /// </summary>
        private readonly IGenreRepository _genreRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="genreRepository">Репозиторий жанров</param>
        public GenreService(IMapper mapper,
                            IGenreRepository genreRepository)
        {
            _mapper = mapper;
            _genreRepository = genreRepository;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<GenreDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<GenreDto>>(await _genreRepository.GetListAsync());
        }

        /// <inheritdoc />
        public async Task<IEnumerable<GenreStatisticDto>> GetStatisticAsync()
        {
            var statistic = await _genreRepository.GetStatisticAsync();

            return statistic.Select(s => new GenreStatisticDto
            {
                Genre = _mapper.Map<GenreDto>(s.Item1),
                BookCount = s.Item2
            });
        }

        /// <inheritdoc />
        public async Task<GenreDto> CreateAsync(CreateGenreDto dto)
        {
            var entity = _mapper.Map<Genre>(dto);
            entity = await _genreRepository.AddAsync(entity);
            return _mapper.Map<GenreDto>(entity);
        }
    }
}
