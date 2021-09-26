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
        /// Получение книг на руках у человека
        /// </summary>
        /// <param name="entity">Сущность человека</param>
        /// <param name="bookId">Идентификатор получаемой книги</param>
        Task<Person> ReceiveBookAsync(Person entity, int bookId);

        /// <summary>
        /// Получение книг на руках у человека
        /// </summary>
        /// <param name="entity">Сущность человека</param>
        /// <param name="bookId">Идентификатор возвращаемой книги</param>
        Task<Person> ReturnBookAsync(Person entity, int bookId);
    }
}
