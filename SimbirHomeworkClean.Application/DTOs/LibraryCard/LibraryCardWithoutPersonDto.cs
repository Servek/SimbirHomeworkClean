using System;
using FluentValidation;
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
        public BookDto Book { get; set; }

        /// <summary>
        /// Дата и время получение книги
        /// </summary>
        public DateTimeOffset ObtainedDateTime { get; set; }
    }

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор транспортного объекта записи о получении книги человеком
    /// </summary>
    public class LibraryCardWithoutPersonDtoValidator : AbstractValidator<LibraryCardWithoutPersonDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public LibraryCardWithoutPersonDtoValidator()
        {
            RuleFor(x => x.Book).NotNull();
            RuleFor(x => x.ObtainedDateTime).NotNull();
        }
    }
}
