using FluentValidation;
using SimbirHomeworkClean.Application.DTOs.Person.Base;

namespace SimbirHomeworkClean.Application.DTOs.Person
{
    /// <summary>
    /// Транспортный объект создания человека
    /// </summary>
    public class CreatePersonDto : BasePersonDto { }

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор транспортного объекта создания человека
    /// </summary>
    public class CreatePersonDtoValidator : AbstractValidator<CreatePersonDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public CreatePersonDtoValidator()
        {
            Include(new BasePersonDtoValidator());
        }
    }
}
