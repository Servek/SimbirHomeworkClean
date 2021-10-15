using FluentValidation;
using SimbirHomeworkClean.Application.DTOs.Author.Base;

namespace SimbirHomeworkClean.Application.DTOs.Author
{
    /// <summary>
    /// Транспортный объект создания автора
    /// </summary>
    public class CreateAuthorDto : BaseAuthorDto { }

    /// <summary>
    /// Валидатор транспортного объекта создания автора
    /// </summary>
    public class CreateAuthorDtoValidator : AbstractValidator<CreateAuthorDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public CreateAuthorDtoValidator()
        {
            Include(new BaseAuthorDtoValidator());
        }
    }
}
