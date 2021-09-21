using System.Collections.Generic;
using SimbirHomeworkClean.Application.DTOs.Book.Base;
using SimbirHomeworkClean.Application.DTOs.Genre;

namespace SimbirHomeworkClean.Application.DTOs.Book
{
    /// <summary>
    /// Транспортный объект создания книги без автора
    /// </summary>
    public class CreateBookWithoutAuthorDto : BaseBookDto
    {
        /// <summary>
        /// Жанры
        /// </summary>
        public IEnumerable<CreateGenreDto> Genres { get; set; }
    }
}
