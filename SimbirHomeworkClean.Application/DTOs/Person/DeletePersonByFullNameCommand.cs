using System.ComponentModel.DataAnnotations;
using SimbirHomeworkClean.Application.DTOs.Person.Base;

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
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }
    }
}
