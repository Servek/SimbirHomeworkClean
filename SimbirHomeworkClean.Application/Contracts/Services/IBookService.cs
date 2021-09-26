using System.Collections.Generic;
using System.Threading.Tasks;
using SimbirHomeworkClean.Application.DTOs.Book;

namespace SimbirHomeworkClean.Application.Contracts.Services
{
    /// <summary>
    /// Интерфейс сервиса книг
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Получить отфильтрованные книги
        /// </summary>
        /// <param name="query">Запрос на список книг</param>
        /// <returns>Перечень полных транспортных объектов книг</returns>
        Task<IEnumerable<FullBookDto>> GetFilteredAsync(BooksQuery query);
        
        /// <summary>
        /// Создать книгу с жанрами
        /// </summary>
        /// <param name="dto">Транспортный объект создания книги с жанрами</param>
        /// <returns>Полный транспортный объект книги</returns>
        Task<FullBookDto> CreateAsync(CreateBookWithGenresDto dto);
        
        /// <summary>
        /// Обновить книгу с жанрами
        /// </summary>
        /// <param name="id">Идентификатор книги</param>
        /// <param name="dto">Транспортный объект создания книги с жанрами</param>
        /// <returns>Полный транспортный объект книги</returns>
        Task<FullBookDto> UpdateAsync(int id, CreateBookWithGenresDto dto);
        
        /// <summary>
        /// Удалить книгу по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор книги</param>
        Task DeleteAsync(int id);
     }
}
