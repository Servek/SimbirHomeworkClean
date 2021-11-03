using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimbirHomeworkClean.Application.Contracts.Repositories.Base
{
    /// <summary>
    /// Интерфейс базового репозитория
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IRepository<T>
        where T : class
    {
        /// <summary>
        /// Получение полного списка записей
        /// </summary>
        Task<List<T>> GetListAsync();

        /// <summary>
        /// Получение записи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="entity">Сущность</param>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Добавление набора записей
        /// </summary>
        /// <param name="entities">Сущности</param>
        Task<List<T>> AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="entity">Сущность</param>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="entity">Сущность</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Удаление набора записей
        /// </summary>
        /// <param name="entities">Сущности</param>
        Task DeleteRangeAsync(IEnumerable<T> entities);
    }
}
