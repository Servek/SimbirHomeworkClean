using System;
using FluentValidation;

namespace SimbirHomeworkClean.Application.DTOs.Person.Base
{
    /// <summary>
    /// Базовый транспортный объект человека
    /// </summary>
    public abstract class BasePersonDto
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

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }
    }

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор базового транспортного объекта человека
    /// </summary>
    public class BasePersonDtoValidator : AbstractValidator<BasePersonDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public BasePersonDtoValidator()
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.BirthDate).NotNull();
        }
    }
}
