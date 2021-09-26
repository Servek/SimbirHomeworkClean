using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimbirHomeworkClean.Application.Contracts.Services;
using SimbirHomeworkClean.Application.DTOs.Genre;

namespace SimbirHomeworkClean.Api.Controllers
{
    // Пункт задания: 7.4.
    /// <summary>
    /// Контроллер жанра
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {
        /// <summary>
        /// Сервис жанров
        /// </summary>
        private readonly IGenreService _genreService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="genreService">Сервис жанров</param>
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // Пункт задания: 7.4.1.
        /// <summary>
        /// Список жанров
        /// </summary>
        /// <returns>Список транспортных объектов жанра</returns>
        /// <response code="200">Список успешно возвращён</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GenreDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get()
        {
            try
            {
                var result = await _genreService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Пункт задания: 7.4.3.
        /// <summary>
        /// Статистика по жанрам
        /// </summary>
        /// <returns>Список транспортных объектов статистики по жанрам</returns>
        /// <response code="200">Список успешно возвращён</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<GenreStatisticDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Statistic()
        {
            try
            {
                var result = await _genreService.GetStatisticAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Пункт задания: 7.4.2.
        /// <summary>
        /// Создание жанра
        /// </summary>
        /// <param name="dto">Объект на создание жанра</param>
        /// <returns>Транспортный объект жанра</returns>
        /// <response code="201">Жанр успешно создан</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpPost]
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] CreateGenreDto dto)
        {
            try
            {
                var result = await _genreService.CreateAsync(dto);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
