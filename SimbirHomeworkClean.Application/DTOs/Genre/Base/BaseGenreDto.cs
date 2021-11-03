using FluentValidation;

namespace SimbirHomeworkClean.Application.DTOs.Genre.Base
{
    /// <summary>
    /// Базовый транспортный объект жанра
    /// </summary>
    public abstract class BaseGenreDto
    {
        /// <summary>
        /// Название
        /// </summary>
        public string GenreName { get; set; }
    }

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор транспортного объекта жанра
    /// </summary>
    public class BaseGenreDtoValidator : AbstractValidator<BaseGenreDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public BaseGenreDtoValidator()
        {
            RuleFor(x => x.GenreName).NotNull();
        }
    }
}
