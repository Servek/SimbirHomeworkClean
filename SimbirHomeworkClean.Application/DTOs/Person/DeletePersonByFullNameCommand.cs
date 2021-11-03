using FluentValidation;

namespace SimbirHomeworkClean.Application.DTOs.Person
{
    /// <summary>
    /// Команда на удаления человека по ФИО
    /// </summary>
    public class DeletePersonByFullNameCommand
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

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор команды на удаления человека по ФИО
    /// </summary>
    public class DeletePersonByFullNameCommandValidator : AbstractValidator<DeletePersonByFullNameCommand>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public DeletePersonByFullNameCommandValidator()
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
        }
    }
}
