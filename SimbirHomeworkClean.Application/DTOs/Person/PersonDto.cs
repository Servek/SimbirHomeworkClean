using System;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public int Id { get; set; }
    }
}
