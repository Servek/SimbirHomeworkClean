using System.Collections.Generic;
using System.Threading.Tasks;
using SimbirHomeworkClean.Application.Contracts.Repositories.Base;
using SimbirHomeworkClean.Application.Filters;
using SimbirHomeworkClean.Domain.Entities;

namespace SimbirHomeworkClean.Application.Contracts.Repositories
{
    /// <summary>
    /// Интерфейс репозитория людей
    /// </summary>
    public interface IPersonRepository : IRepository<Person>
    {
        /// <summary>
        /// Получение отфильтрованного списка людей
        /// </summary>
        Task<List<Person>> GetFilteredListAsync(PersonFilter filter);

        /// <summary>
        /// Получение книг (записей о получении) на руках у человека
        /// </summary>
        /// <param name="id">Идентификатор человека</param>
        Task<List<LibraryCard>> GetLibraryCardsAsync(int id);

        /// <summary>
        /// Добавление записи о получении книги человеком
        /// </summary>
        /// <param name="id">Идентификатор человека</param>
        /// <param name="bookId">Идентификатор получаемой книги</param>
        Task<Person> ReceiveBookAsync(int id, int bookId);

        /// <summary>
        /// Удаление записи о получении книги человеком
        /// </summary>
        /// <param name="id">Идентификатор человека</param>
        /// <param name="bookId">Идентификатор возвращаемой книги</param>
        Task<Person> ReturnBookAsync(int id, int bookId);
    }
}
