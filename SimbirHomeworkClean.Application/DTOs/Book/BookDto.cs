using System.ComponentModel.DataAnnotations;
using SimbirHomeworkClean.Application.DTOs.Book.Base;

namespace SimbirHomeworkClean.Application.DTOs.Book
{
    /// <summary>
    /// Транспортный объект книги
    /// </summary>
    public class BookDto : BaseBookDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}
