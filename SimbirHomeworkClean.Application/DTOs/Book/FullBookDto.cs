using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimbirHomeworkClean.Application.DTOs.Author;
using SimbirHomeworkClean.Application.DTOs.Book.Base;
using SimbirHomeworkClean.Application.DTOs.Genre;

namespace SimbirHomeworkClean.Application.DTOs.Book
{
    /// <summary>
    /// Полный транспортный объект книги
    /// </summary>
    public class FullBookDto : BaseBookDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        [Required]
        public AuthorDto Author { get; set; }

        /// <summary>
        /// Жанры
        /// </summary>
        public IEnumerable<GenreDto> Genres { get; set; }
    }
}
