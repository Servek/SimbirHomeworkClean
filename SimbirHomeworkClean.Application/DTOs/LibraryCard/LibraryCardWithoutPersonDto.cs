using System;
using System.ComponentModel.DataAnnotations;
using SimbirHomeworkClean.Application.DTOs.Book;

namespace SimbirHomeworkClean.Application.DTOs.LibraryCard
{
    /// <summary>
    /// Транспортный объект записи о получении книги человеком
    /// </summary>
    public class LibraryCardWithoutPersonDto
    {
        /// <summary>
        /// Книга
        /// </summary>
        [Required]
        public BookDto Book { get; set; }

        /// <summary>
        /// Дата и время получение книги
        /// </summary>
        [Required]
        public DateTimeOffset ObtainedDateTime { get; set; }
    }
}
