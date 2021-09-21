using System.Collections.Generic;
using SimbirHomeworkClean.Application.DTOs.Author.Base;
using SimbirHomeworkClean.Application.DTOs.Book;

namespace SimbirHomeworkClean.Application.DTOs.Author
{
    /// <summary>
    /// Транспортный объект создания автора с книгами
    /// </summary>
    public class CreateAuthorWithBooksDto : BaseAuthorDto
    {
        /// <summary>
        /// Книги автора
        /// </summary>
        public IEnumerable<CreateBookWithoutAuthorDto> Books { get; set; }
    }
}
