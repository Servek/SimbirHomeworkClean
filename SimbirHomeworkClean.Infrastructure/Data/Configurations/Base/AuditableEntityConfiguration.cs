using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimbirHomeworkClean.Domain.Entities.Base;

// Пункт задания: 3
namespace SimbirHomeworkClean.Infrastructure.Data.Configurations.Base
{
    /// <summary>
    /// Базовая настройка аудируемой сущности в базе данных
    /// </summary>
    /// <typeparam name="T">Тип аудируемой сущности</typeparam>
    public abstract class AuditableEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : AuditableEntity
    {
        /// <inheritdoc />
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            // Пункт задания: 9.1.3.
            builder.Property(p => p.RowVersion)
                   .IsRowVersion();
        }
    }
}
