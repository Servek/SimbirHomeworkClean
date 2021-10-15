using System.Collections.Generic;
using FluentValidation;
using SimbirHomeworkClean.Application.DTOs.LibraryCard;
using SimbirHomeworkClean.Application.DTOs.Person.Base;

namespace SimbirHomeworkClean.Application.DTOs.Person
{
    /// <summary>
    /// Транспортный объект человека с записями о получении книг
    /// </summary>
    public class PersonWithLibraryCardsDto : BasePersonDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Записи о получении книг человеком
        /// </summary>
        public IEnumerable<LibraryCardWithoutPersonDto> LibraryCards { get; set; }
    }

    // Лекции 4-5. Пункт задания: 1
    /// <summary>
    /// Валидатор транспортного объекта человека с записями о получении книг
    /// </summary>
    public class PersonWithLibraryCardsDtoValidator : AbstractValidator<PersonWithLibraryCardsDto>
    {
        /// <summary>
        /// Правила валидатора
        /// </summary>
        public PersonWithLibraryCardsDtoValidator()
        {
            Include(new BasePersonDtoValidator());
            RuleFor(x => x.Id).NotNull();
        }
    }
}
