using FluentAssertions.Equivalency;
using SimbirHomeworkClean.Domain.Entities.Base;

namespace SimbirHomeworkClean.Infrastructure.UnitTests.Extensions
{
    /// <summary>
    /// Расширение <see cref="EquivalencyAssertionOptions"/>
    /// </summary>
    public static class EquivalencyAssertionOptionsExtension
    {
        /// <summary>
        /// Исключение служебных (для аудита) членов
        /// </summary>
        /// <param name="options">Параметры утрерждения эквивалентности</param>
        /// <typeparam name="TAuditable">Аудируемый тип сущности</typeparam>
        /// <returns>Параметры утрерждения эквивалентности</returns>
        public static EquivalencyAssertionOptions<TAuditable> ExcludingAuditingMembers<TAuditable>(this EquivalencyAssertionOptions<TAuditable> options)
            where TAuditable : AuditableEntity
        {
            return options.Excluding(e => e.Created)
                          .Excluding(e => e.Updated)
                          .Excluding(e => e.RowVersion);
        }
    }
}
