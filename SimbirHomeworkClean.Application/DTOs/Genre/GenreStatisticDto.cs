using System.ComponentModel.DataAnnotations;

namespace SimbirHomeworkClean.Application.DTOs.Genre
{
    /// <summary>
    /// Транспортный объект статистики по жанру
    /// </summary>
    public class GenreStatisticDto
    {
        /// <summary>
        /// Жанр
        /// </summary>
        [Required]
        public GenreDto Genre { get; set; }

        /// <summary>
        /// Количество книг
        /// </summary>
        [Required]
        public int BookCount { get; set; }
    }
}
