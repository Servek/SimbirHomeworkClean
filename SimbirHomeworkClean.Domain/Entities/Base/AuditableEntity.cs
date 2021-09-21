using System;

namespace SimbirHomeworkClean.Domain.Entities.Base
{
    // Пункт задания: 9.1.
    /// <summary>
    /// Базовый класс аудируемой сущности
    /// </summary>
    public abstract class AuditableEntity
    {
        // Пункт задания: 9.1.1.
        /// <summary>
        /// Дата и время вставки записи
        /// </summary>
        public DateTimeOffset Created { get; set; }

        // Пункт задания: 9.1.2.
        /// <summary>
        /// Дата и время последнего изменения записи
        /// </summary>
        public DateTimeOffset Updated { get; set; }

        // Пункт задания: 9.1.3.
        /// <summary>
        /// Версия записи
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}
