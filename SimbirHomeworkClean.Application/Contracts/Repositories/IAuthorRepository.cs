using System.Collections.Generic;
using System.Threading.Tasks;
using SimbirHomeworkClean.Application.Contracts.Repositories.Base;
using SimbirHomeworkClean.Application.Filters;
using SimbirHomeworkClean.Domain.Entities;

namespace SimbirHomeworkClean.Application.Contracts.Repositories
{
    /// <summary>
    /// Интерфейс репозитория авторов
    /// </summary>
    public interface IAuthorRepository : IRepository<Author>
    {
        /// <summary>
        /// Получение отфильтрованного списка авторов
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="isDescOrder">Является ли сортировка по убыванию</param>
        Task<List<Author>> GetFilteredListAsync(AuthorFilter filter, bool isDescOrder);

        /// <summary>
        /// Получение автора с книгами по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        Task<Author> GetByIdWithBooksAsync(int id);
    }
}
