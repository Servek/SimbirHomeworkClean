using System.Collections.Generic;
using FluentValidation;
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

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор транспортного объекта создания автора с книгами
    /// </summary>
    public class CreateAuthorWithBooksDtoValidator : AbstractValidator<CreateAuthorWithBooksDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public CreateAuthorWithBooksDtoValidator()
        {
            Include(new BaseAuthorDtoValidator());
        }
    }
}
