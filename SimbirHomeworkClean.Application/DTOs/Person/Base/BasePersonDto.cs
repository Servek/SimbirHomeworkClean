using System;
using System.ComponentModel.DataAnnotations;

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

        /// <summary>
        /// Дата рождения
        /// </summary>
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
