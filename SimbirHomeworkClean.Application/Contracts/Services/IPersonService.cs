using System.Collections.Generic;
using System.Threading.Tasks;
using SimbirHomeworkClean.Application.DTOs.Book;
using SimbirHomeworkClean.Application.DTOs.LibraryCard;
using SimbirHomeworkClean.Application.DTOs.Person;

namespace SimbirHomeworkClean.Application.Contracts.Services
{
    /// <summary>
    /// Интерфейс сервиса людей
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Создать человека
        /// </summary>
        /// <param name="dto">Транспортный объект создания человека</param>
        /// <returns>Транспортный объект человека</returns>
        Task<PersonDto> CreateAsync(CreatePersonDto dto);

        /// <summary>
        /// Обновить человека
        /// </summary>
        /// <param name="id">Идентификатор человека</param>
        /// <param name="dto">Транспортный объект обновления человека</param>
        /// <returns>Транспортный объект человека</returns>
        Task<PersonDto> UpdateAsync(int id, CreatePersonDto dto);

        /// <summary>
        /// Удалить человека по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор человека</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Удалить человека по ФИО
        /// </summary>
        /// <param name="command">Команда на удаление человека по ФИО</param>
        Task DeleteByFullNameAsync(DeletePersonByFullNameCommand command);

        /// <summary>
        /// Получить все взятые человеком книги (записи о получении)
        /// </summary>
        /// <param name="id">Идентификатор человека</param>
        /// <returns>Перечень транспортных объектов жанров</returns>
        Task<IEnumerable<LibraryCardWithoutPersonDto>> GetPersonLibraryCardsAsync(int id);

        /// <summary>
        /// Получить книгу
        /// </summary>
        /// <param name="id">Идентификатор человека</param>
        /// <param name="bookId">Идентификатор получаемой книги</param>
        /// <returns>Транспортный объект человека</returns>
        Task<PersonWithLibraryCardsDto> ReceiveBookAsync(int id, int bookId);

        /// <summary>
        /// Вернуть книгу
        /// </summary>
        /// <param name="id">Идентификатор человека</param>
        /// <param name="bookId">Идентификатор возвращаемой книги</param>
        /// <returns>Транспортный объект человека</returns>
        Task<PersonWithLibraryCardsDto> ReturnBookAsync(int id, int bookId);
    }
}
