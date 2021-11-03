using FluentValidation;

namespace SimbirHomeworkClean.Application.DTOs.Genre
{
    /// <summary>
    /// Транспортный объект статистики по жанру
    /// </summary>
    public class GenreStatisticDto
    {
        /// <summary>
        /// Жанр
        /// </summary>
        public GenreDto Genre { get; set; }

        /// <summary>
        /// Количество книг
        /// </summary>
        public int BookCount { get; set; }
    }

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор транспортного объекта статистики по жанру
    /// </summary>
    public class GenreStatisticDtoValidator : AbstractValidator<GenreStatisticDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public GenreStatisticDtoValidator()
        {
            RuleFor(x => x.Genre).NotNull();
            RuleFor(x => x.BookCount).NotNull();
        }
    }
}
