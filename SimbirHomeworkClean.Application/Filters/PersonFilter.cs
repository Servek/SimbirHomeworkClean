namespace SimbirHomeworkClean.Application.Filters
{
    /// <summary>
    /// Фильтр людей
    /// </summary>
    public class PersonFilter
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
}
