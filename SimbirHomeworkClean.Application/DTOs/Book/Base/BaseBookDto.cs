using FluentValidation;

namespace SimbirHomeworkClean.Application.DTOs.Book.Base
{
    /// <summary>
    /// Базовый транспортный объект книги без идентификатора и внешних сущностей
    /// </summary>
    public abstract class BaseBookDto
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Год написания
        /// </summary>
        public int WritingYear { get; set; }
    }

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор транспортного объекта книги без идентификатора и внешних сущностей
    /// </summary>
    public class BaseBookDtoValidator : AbstractValidator<BaseBookDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public BaseBookDtoValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.WritingYear).NotNull();
        }
    }
}
