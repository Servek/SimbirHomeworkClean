using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimbirHomeworkClean.Domain.Entities;
using SimbirHomeworkClean.Infrastructure.Data.Configurations.Base;

// Пункт задания: 3
namespace SimbirHomeworkClean.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Настройка сущности человека
    /// </summary>
    public class PersonConfiguration : AuditableEntityConfiguration<Person>
    {
        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.FirstName)
                   .IsRequired();

            builder.Property(p => p.LastName)
                   .IsRequired();
            
            base.Configure(builder);
        }
    }
}
