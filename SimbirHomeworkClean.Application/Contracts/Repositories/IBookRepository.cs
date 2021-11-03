using System.Collections.Generic;
using System.Threading.Tasks;
using SimbirHomeworkClean.Application.Contracts.Repositories.Base;
using SimbirHomeworkClean.Application.Filters;
using SimbirHomeworkClean.Domain.Entities;

namespace SimbirHomeworkClean.Application.Contracts.Repositories
{
    /// <summary>
    /// Интерфейс репозитория книг
    /// </summary>
    public interface IBookRepository : IRepository<Book>
    {
        /// <summary>
        /// Получение отфильтрованного списка книг
        /// </summary>
        Task<List<Book>> GetFilteredListAsync(BookFilter filter);

        /// <summary>
        /// Получение книги с записями о получении книги людьми
        /// </summary>
        /// <param name="id">Идентификатор</param>
        Task<Book> GetByIdWithLibraryCardsAsync(int id);

        /// <summary>
        /// Обновление жанров книги
        /// </summary>
        /// <param name="id">Идентификатор</param>
        Task<Book> GetByIdWithGenresAsync(int id);
    }
}
