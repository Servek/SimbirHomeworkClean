using System.Collections.Generic;
using FluentValidation;
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
        public int Id { get; set; }

        /// <summary>
        /// Жанры
        /// </summary>
        public IEnumerable<GenreDto> Genres { get; set; }
    }

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор транспортного объекта книги с жанрами
    /// </summary>
    public class BookWithGanresDtoValidator : AbstractValidator<BookWithGanresDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public BookWithGanresDtoValidator()
        {
            Include(new BaseBookDtoValidator());
            RuleFor(x => x.Id).NotNull();
        }
    }
}
