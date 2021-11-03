using FluentValidation;
using SimbirHomeworkClean.Application.DTOs.Genre.Base;

namespace SimbirHomeworkClean.Application.DTOs.Genre
{
    /// <summary>
    /// Транспортный объект создания жанра
    /// </summary>
    public class CreateGenreDto : BaseGenreDto { }

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор транспортного объекта создания жанра
    /// </summary>
    public class CreateGenreDtoValidator : AbstractValidator<CreateGenreDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public CreateGenreDtoValidator()
        {
            Include(new BaseGenreDtoValidator());
        }
    }
}
