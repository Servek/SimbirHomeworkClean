using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimbirHomeworkClean.Application.DTOs.Author.Base;
using SimbirHomeworkClean.Application.DTOs.Book;

namespace SimbirHomeworkClean.Application.DTOs.Author
{
    /// <summary>
    /// Полный транспортный объект автора
    /// </summary>
    public class FullAuthorDto : BaseAuthorDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Required]
        public int Id { get; set; }
        
        /// <summary>
        /// Книги автора
        /// </summary>
        public IEnumerable<BookWithGanresDto> Books { get; set; }
    }
}
