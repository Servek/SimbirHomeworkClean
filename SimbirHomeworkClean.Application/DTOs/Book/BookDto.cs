using FluentValidation;
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
        public int Id { get; set; }
    }

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор транспортного объекта книги
    /// </summary>
    public class BookDtoValidator : AbstractValidator<BookDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public BookDtoValidator()
        {
            Include(new BaseBookDtoValidator());
            RuleFor(x => x.Id).NotNull();
        }
    }
}
