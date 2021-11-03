using FluentValidation;
using SimbirHomeworkClean.Application.DTOs.Genre.Base;

namespace SimbirHomeworkClean.Application.DTOs.Genre
{
    /// <summary>
    /// Транспортный объект жанра
    /// </summary>
    public class GenreDto : BaseGenreDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
    }

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор транспортного объекта жанра
    /// </summary>
    public class GenreDtoValidator : AbstractValidator<GenreDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public GenreDtoValidator()
        {
            Include(new BaseGenreDtoValidator());
            RuleFor(x => x.Id).NotNull();
        }
    }
}
