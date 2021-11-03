using System.Collections.Generic;
using FluentValidation;
using SimbirHomeworkClean.Application.DTOs.Author;
using SimbirHomeworkClean.Application.DTOs.Book.Base;
using SimbirHomeworkClean.Application.DTOs.Genre;

namespace SimbirHomeworkClean.Application.DTOs.Book
{
    /// <summary>
    /// Полный транспортный объект книги
    /// </summary>
    public class FullBookDto : BaseBookDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        public AuthorDto Author { get; set; }

        /// <summary>
        /// Жанры
        /// </summary>
        public IEnumerable<GenreDto> Genres { get; set; }
    }

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор полного транспортного объекта книги
    /// </summary>
    public class FullBookDtoValidator : AbstractValidator<FullBookDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public FullBookDtoValidator()
        {
            Include(new BaseBookDtoValidator());
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Author).NotNull();
        }
    }
}
