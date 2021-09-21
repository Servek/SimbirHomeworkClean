using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Записи о получении книг человеком
        /// </summary>
        public IEnumerable<LibraryCardWithoutPersonDto> LibraryCards { get; set; }
    }
}
