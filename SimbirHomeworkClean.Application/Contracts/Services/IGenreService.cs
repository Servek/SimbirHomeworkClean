using System.Collections.Generic;
using System.Threading.Tasks;
using SimbirHomeworkClean.Application.DTOs.Genre;

namespace SimbirHomeworkClean.Application.Contracts.Services
{
    /// <summary>
    /// Интерфейс сервиса жанров
    /// </summary>
    public interface IGenreService
    {
        /// <summary>
        /// Получить все жанры
        /// </summary>
        /// <returns>Перечень транспортных объектов жанров</returns>
        Task<IEnumerable<GenreDto>> GetAllAsync();

        /// <summary>
        /// Получить статистику по всем жанрам
        /// </summary>
        /// <returns>Перечень транспортных объектов статистики жанров</returns>
        Task<IEnumerable<GenreStatisticDto>> GetStatisticAsync();

        /// <summary>
        /// Создать жанр
        /// </summary>
        /// <param name="dto">Транспортный объект создания жанра</param>
        /// <returns>Транспортный объект жанра</returns>
        Task<GenreDto> CreateAsync(CreateGenreDto dto);
    }
}
