using FluentValidation;

namespace SimbirHomeworkClean.Application.DTOs.Author.Base
{
    /// <summary>
    /// Базовый транспортный объект автора без идентификатора и внешних сущностей
    /// </summary>
    public abstract class BaseAuthorDto
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }
    }

    /// <summary>
    /// Валидатор транспортного объекта автора без идентификатора и внешних сущностей
    /// </summary>
    public class BaseAuthorDtoValidator : AbstractValidator<BaseAuthorDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public BaseAuthorDtoValidator()
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
        }
    }
}
