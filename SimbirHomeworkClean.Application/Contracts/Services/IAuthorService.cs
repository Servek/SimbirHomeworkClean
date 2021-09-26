using System.Collections.Generic;
using System.Threading.Tasks;
using SimbirHomeworkClean.Application.DTOs.Author;

namespace SimbirHomeworkClean.Application.Contracts.Services
{
    /// <summary>
    /// Интерфейс сервиса авторов
    /// </summary>
    public interface IAuthorService
    {
        /// <summary>
        /// Получить всех авторов
        /// </summary>
        /// <returns>Перечень транспортных объектов авторов</returns>
        Task<IEnumerable<AuthorDto>> GetAllAsync();

        /// <summary>
        /// Получить отфильтрованных авторов
        /// </summary>
        /// <param name="query">Запрос на список авторов</param>
        /// <returns>Перечень транспортных объектов авторов</returns>
        Task<IEnumerable<AuthorDto>> GetFilteredAsync(AuthorsQuery query);

        /// <summary>
        /// Получить автора по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор автора</param>
        /// <returns>Транспортный объект автора с книгами</returns>
        Task<FullAuthorDto> GetByIdAsync(int id);

        /// <summary>
        /// Создать автора с книгами
        /// </summary>
        /// <param name="dto">Транспортный объект создания автора с книгами</param>
        /// <returns>Транспортный объект автора с книгами</returns>
        Task<FullAuthorDto> CreateAsync(CreateAuthorWithBooksDto dto);

        /// <summary>
        /// Удалить автора по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор автора</param>
        Task DeleteAsync(int id);
    }
}
