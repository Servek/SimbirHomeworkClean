using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimbirHomeworkClean.Application.Contracts.Services;
using SimbirHomeworkClean.Application.DTOs.Author;

namespace SimbirHomeworkClean.Api.Controllers
{
    // Пункт задания: 7.3.
    /// <summary>
    /// Контроллер автора
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        /// <summary>
        /// Сервис авторов
        /// </summary>
        private readonly IAuthorService _authorService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="authorService">Сервис авторов</param>
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // Пункт задания: 7.3.1.
        /// <summary>
        /// Список авторов
        /// </summary>
        /// <returns>Список транспортных объектов автора</returns>
        /// <response code="200">Список успешно возвращён</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AuthorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get()
        {
            try
            {
                var result = await _authorService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Пункт задания: 8.2. и 8.3.
        /// <summary>
        /// Отфильтрованный список авторов
        /// </summary>
        /// <param name="query">Запрос авторов</param>
        /// <returns>Список транспортных объектов автора</returns>
        /// <response code="200">Список успешно возвращён</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<AuthorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetFiltered([FromQuery] AuthorsQuery query)
        {
            try
            {
                var result = await _authorService.GetFilteredAsync(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Пункт задания: 7.3.2.
        /// <summary>
        /// Информация об авторе
        /// </summary>
        /// <param name="id">Идентификатор автора</param>
        /// <returns>Информация об авторе</returns>
        /// <response code="200">Информация успешно возвращена</response>
        /// <response code="400">Неверный запрос</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(FullAuthorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FullAuthorDto>> Get([FromRoute] int id)
        {
            try
            {
                var result = await _authorService.GetByIdAsync(id);
                return Ok(result);
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

        // Пункт задания: 7.3.3.
        /// <summary>
        /// Создание автора
        /// </summary>
        /// <param name="dto">Объект создания автора</param>
        /// <returns>Транспортный объект автора</returns>
        /// <response code="201">Автор успешно создан</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpPost]
        [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthorDto>> Post([FromBody] CreateAuthorWithBooksDto dto)
        {
            try
            {
                var result = await _authorService.CreateAsync(dto);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Пункт задания: 7.3.4.
        /// <summary>
        /// Удаление автора
        /// </summary>
        /// <param name="id">Идентификатор автора</param>
        /// <response code="204">Автор успешно удалён</response>
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
                await _authorService.DeleteAsync(id);
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
