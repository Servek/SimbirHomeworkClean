using System.Collections.Generic;
using FluentValidation;
using SimbirHomeworkClean.Application.DTOs.Book.Base;
using SimbirHomeworkClean.Application.DTOs.Genre;

namespace SimbirHomeworkClean.Application.DTOs.Book
{
    /// <summary>
    /// Транспортный объект создания книги без автора
    /// </summary>
    public class CreateBookWithoutAuthorDto : BaseBookDto
    {
        /// <summary>
        /// Жанры
        /// </summary>
        public IEnumerable<CreateGenreDto> Genres { get; set; }
    }

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор транспортного объекта создания книги без автора
    /// </summary>
    public class CreateBookWithoutAuthorDtoValidator : AbstractValidator<CreateBookWithoutAuthorDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public CreateBookWithoutAuthorDtoValidator()
        {
            Include(new BaseBookDtoValidator());
        }
    }
}
