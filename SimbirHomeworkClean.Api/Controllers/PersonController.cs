using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimbirHomeworkClean.Application.Contracts.Services;
using SimbirHomeworkClean.Application.DTOs.LibraryCard;
using SimbirHomeworkClean.Application.DTOs.Person;

namespace SimbirHomeworkClean.Api.Controllers
{
    // Пункт задания: 7.1.
    /// <summary>
    /// Контроллер человека
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        /// <summary>
        /// Сервис людей
        /// </summary>
        private readonly IPersonService _personService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="personService">Сервис людей</param>
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        // Пункт задания: 7.1.1.
        /// <summary>
        /// Создание человека
        /// </summary>
        /// <param name="dto">Объект на создание человека</param>
        /// <returns>Транспортный объект человека</returns>
        /// <response code="201">Человек успешно создан</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpPost]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonDto>> Post([FromBody] CreatePersonDto dto)
        {
            try
            {
                var result = await _personService.CreateAsync(dto);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Пункт задания: 7.1.2.
        /// <summary>
        /// Изменение человека
        /// </summary>
        /// <param name="id">Идентификатор человека</param>
        /// <param name="dto">Объект на создание человека</param>
        /// <returns>Транспортный объект человека</returns>
        /// <response code="201">Человек успешно изменён</response>
        /// <response code="400">Неверный запрос</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonDto>> Put([FromRoute] int id, [FromBody] CreatePersonDto dto)
        {
            try
            {
                var result = await _personService.UpdateAsync(id, dto);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Пункт задания: 7.1.3.
        /// <summary>
        /// Удаление человека
        /// </summary>
        /// <param name="id">Идентификатор человека</param>
        /// <response code="204">Человек успешно удалён</response>
        /// <response code="400">Неверный запрос</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _personService.DeleteAsync(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Пункт задания: 7.1.4.
        /// <summary>
        /// Удаление человека по ФИО
        /// </summary>
        /// <param name="command">Команда на удаление человека</param>
        /// <response code="204">Человек успешно удалён</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpDelete("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteByFullName([FromBody] DeletePersonByFullNameCommand command)
        {
            try
            {
                await _personService.DeleteByFullNameAsync(command);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Пункт задания: 7.1.5.
        /// <summary>
        /// Полученные человеком книги (записи о получении)
        /// </summary>
        /// <param name="id">Идентификатор человека</param>
        /// <returns>Список транспортных объектов записей о получении книги</returns>
        /// <response code="200">Список успешно возвращён</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpGet("{id:int}/Book")]
        [ProducesResponseType(typeof(IEnumerable<LibraryCardWithoutPersonDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LibraryCardWithoutPersonDto>>> GetPersonLibraryCards([FromRoute] int id)
        {
            try
            {
                var result = await _personService.GetPersonLibraryCardsAsync(id);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Пункт задания: 7.1.6.
        /// <summary>
        /// Получить книгу
        /// </summary>
        /// <param name="id">Идентификатор человека</param>
        /// <param name="bookId">Идентификатор книги</param>
        /// <returns>Транспортный объект человека</returns>
        /// <response code="201">Книга успешно добавлена пользователю</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpPost("{id:int}/Book")]
        [ProducesResponseType(typeof(PersonWithLibraryCardsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonWithLibraryCardsDto>> ReceiveBook([FromRoute] int id, [FromBody] int bookId)
        {
            try
            {
                var result = await _personService.ReceiveBookAsync(id, bookId);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Пункт задания: 7.1.7.
        /// <summary>
        /// Вернуть книгу
        /// </summary>
        /// <param name="id">Идентификатор человека</param>
        /// <param name="bookId">Идентификатор книги</param>
        /// <returns>Транспортный объект человека</returns>
        /// <response code="201">Книга успешно убрана у пользователя</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpDelete("{id:int}/Book")]
        [ProducesResponseType(typeof(PersonWithLibraryCardsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonWithLibraryCardsDto>> ReturnBook([FromRoute] int id, [FromBody] int bookId)
        {
            try
            {
                var result = await _personService.ReturnBookAsync(id, bookId);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
