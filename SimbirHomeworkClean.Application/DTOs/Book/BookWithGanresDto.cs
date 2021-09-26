using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimbirHomeworkClean.Application.DTOs.Book.Base;
using SimbirHomeworkClean.Application.DTOs.Genre;

namespace SimbirHomeworkClean.Application.DTOs.Book
{
    /// <summary>
    /// Транспортный объект книги с жанрами
    /// </summary>
    public class BookWithGanresDto : BaseBookDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Required]
        public int Id { get; set; }
        
        /// <summary>
        /// Жанры
        /// </summary>
        public IEnumerable<GenreDto> Genres { get; set; }
    }
}
