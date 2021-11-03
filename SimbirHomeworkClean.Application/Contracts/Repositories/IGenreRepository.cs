using System.Collections.Generic;
using System.Threading.Tasks;
using SimbirHomeworkClean.Application.Contracts.Repositories.Base;
using SimbirHomeworkClean.Domain.Entities;

namespace SimbirHomeworkClean.Application.Contracts.Repositories
{
    /// <summary>
    /// Интерфейс репозитория жанров
    /// </summary>
    public interface IGenreRepository : IRepository<Genre>
    {
        /// <summary>
        /// Получение записей по названиям жанров
        /// </summary>
        /// <param name="genreNames">Названия жанров</param>
        /// <returns>Перечень жанров</returns>
        Task<List<Genre>> GetListByGenreNamesAsync(IEnumerable<string> genreNames);

        /// <summary>
        /// Получение статистики по жанрам (жанр - количество книг)
        /// </summary>
        /// <returns>Перечень жанров с количеством книг</returns>
        Task<List<(Genre, int)>> GetStatisticAsync();
    }
}
