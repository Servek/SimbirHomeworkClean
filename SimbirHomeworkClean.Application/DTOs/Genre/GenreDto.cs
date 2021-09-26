using System.ComponentModel.DataAnnotations;
using SimbirHomeworkClean.Application.DTOs.Genre.Base;

namespace SimbirHomeworkClean.Application.DTOs.Genre
{
    /// <summary>
    /// Транспортный объект жанра
    /// </summary>
    public class GenreDto : BaseGenreDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}
