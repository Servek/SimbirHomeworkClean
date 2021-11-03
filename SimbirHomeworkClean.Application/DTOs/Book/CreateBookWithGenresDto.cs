using System.Collections.Generic;
using FluentValidation;
using SimbirHomeworkClean.Application.DTOs.Author;
using SimbirHomeworkClean.Application.DTOs.Book.Base;
using SimbirHomeworkClean.Application.DTOs.Genre;

namespace SimbirHomeworkClean.Application.DTOs.Book
{
    /// <summary>
    /// Транспортный объект создания книги с жанрами
    /// </summary>
    public class CreateBookWithGenresDto : BaseBookDto
    {
        /// <summary>
        /// Автор
        /// </summary>
        public CreateAuthorDto Author { get; set; }

        /// <summary>
        /// Жанры
        /// </summary>
        public IEnumerable<CreateGenreDto> Genres { get; set; }
    }

    /// <summary>
    /// Валидатор транспортного объекта создания книги с жанрами
    /// </summary>
    public class CreateBookWithGenresDtoValidator : AbstractValidator<CreateBookWithGenresDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public CreateBookWithGenresDtoValidator()
        {
            RuleFor(x => x.Author).NotNull();
        }
    }
}
