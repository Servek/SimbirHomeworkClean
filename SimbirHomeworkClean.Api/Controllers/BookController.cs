using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimbirHomeworkClean.Application.Contracts.Services;
using SimbirHomeworkClean.Application.DTOs.Book;

namespace SimbirHomeworkClean.Api.Controllers
{
    // Пункт задания: 7.2.
    /// <summary>
    /// Контроллер книги
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        /// <summary>
        /// Сервис книг
        /// </summary>
        private readonly IBookService _bookService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="bookService">Сервис книг</param>
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // Пункт задания: 7.2.4. и 7.2.5.
        /// <summary>
        /// Список книг
        /// </summary>
        /// <param name="query">Запрос книг</param>
        /// <returns>Список транспортных объектов книги</returns>
        /// <response code="200">Список успешно возвращён</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FullBookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FullBookDto>>> GetFiltered([FromQuery] BooksQuery query)
        {
            try
            {
                var result = await _bookService.GetFilteredAsync(query);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Пункт задания: 7.2.1.
        /// <summary>
        /// Создание книги
        /// </summary>
        /// <param name="dto">Объект на создание книги</param>
        /// <returns>Транспортный объект книги</returns>
        /// <response code="201">Книга успешно создана</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpPost]
        [ProducesResponseType(typeof(FullBookDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FullBookDto>> Post([FromBody] CreateBookWithGenresDto dto)
        {
            try
            {
                var result = await _bookService.CreateAsync(dto);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Пункт задания: 7.2.3.
        /// <summary>
        /// Изменение книги
        /// </summary>
        /// <param name="id">Идентификатор книги</param>
        /// <param name="dto">Объект на создание книги</param>
        /// <returns>Транспортный объект книги</returns>
        /// <response code="201">Книга успешно изменена</response>
        /// <response code="400">Неверный запрос</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(FullBookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FullBookDto>> Put([FromRoute] int id, [FromBody] CreateBookWithGenresDto dto)
        {
            try
            {
                var result = await _bookService.UpdateAsync(id, dto);
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

        // Пункт задания: 7.2.2.
        /// <summary>
        /// Удаление книги
        /// </summary>
        /// <param name="id">Идентификатор книги</param>
        /// <response code="204">Книга успешно удалена</response>
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
                await _bookService.DeleteAsync(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (ConstraintException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
