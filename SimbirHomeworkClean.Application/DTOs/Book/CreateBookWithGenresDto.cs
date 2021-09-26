using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimbirHomeworkClean.Application.DTOs.Author;
using SimbirHomeworkClean.Application.DTOs.Book.Base;
using SimbirHomeworkClean.Application.DTOs.Genre;

namespace SimbirHomeworkClean.Application.DTOs.Book
{
    /// <summary>
    /// Транспортный объект создания книги с жанрами
    /// </summary>
    public class CreateBookWithGenresDto : BaseBookDto
    {
        /// <summary>
        /// Автор
        /// </summary>
        [Required]
        public CreateAuthorDto Author { get; set; }

        /// <summary>
        /// Жанры
        /// </summary>
        public IEnumerable<CreateGenreDto> Genres { get; set; }
    }
}
