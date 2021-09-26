namespace SimbirHomeworkClean.Domain.Structs
{
    /// <summary>
    /// Структура ФИО
    /// </summary>
    public struct FullName
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName;

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName;

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="middleName">Отчество</param>
        public FullName(string firstName, string lastName, string middleName)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }
    }
}
