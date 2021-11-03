using FluentValidation;
using SimbirHomeworkClean.Application.DTOs.Author.Base;

namespace SimbirHomeworkClean.Application.DTOs.Author
{
    /// <summary>
    /// Транспортный объект автора
    /// </summary>
    public class AuthorDto : BaseAuthorDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
    }

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор транспортного объекта автора
    /// </summary>
    public class AuthorDtoValidator : AbstractValidator<AuthorDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public AuthorDtoValidator()
        {
            Include(new BaseAuthorDtoValidator());
            RuleFor(x => x.Id).NotNull();
        }
    }
}
