using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
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
        public int Id { get; set; }
        
        /// <summary>
        /// Книги автора
        /// </summary>
        public IEnumerable<BookWithGanresDto> Books { get; set; }
    }

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор полного транспортного объекта автора
    /// </summary>
    public class FullAuthorDtoValidator : AbstractValidator<FullAuthorDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public FullAuthorDtoValidator()
        {
            Include(new BaseAuthorDtoValidator());
            RuleFor(x => x.Id).NotNull();
        }
    }
}
