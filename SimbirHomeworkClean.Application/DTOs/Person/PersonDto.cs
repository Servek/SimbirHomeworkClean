using FluentValidation;
using SimbirHomeworkClean.Application.DTOs.Person.Base;

namespace SimbirHomeworkClean.Application.DTOs.Person
{
    /// <summary>
    /// Транспортный объект человека
    /// </summary>
    public class PersonDto : BasePersonDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
    }

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор транспортного объекта человека
    /// </summary>
    public class PersonDtoValidator : AbstractValidator<PersonDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public PersonDtoValidator()
        {
            Include(new BasePersonDtoValidator());
            RuleFor(x => x.Id).NotNull();
        }
    }
}
