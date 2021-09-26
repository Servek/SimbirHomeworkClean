using System.ComponentModel.DataAnnotations;

namespace SimbirHomeworkClean.Application.DTOs.Genre.Base
{
    /// <summary>
    /// Базовый транспортный объект жанра
    /// </summary>
    public abstract class BaseGenreDto
    {
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        public string GenreName { get; set; }
    }
}
