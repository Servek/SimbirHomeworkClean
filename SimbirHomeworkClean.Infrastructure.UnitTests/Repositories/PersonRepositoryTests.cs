using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimbirHomeworkClean.Application.Filters;
using SimbirHomeworkClean.Infrastructure.Repositories;
using SimbirHomeworkClean.Infrastructure.UnitTests.Fixtures;
using Xunit;

namespace SimbirHomeworkClean.Infrastructure.UnitTests.Repositories
{
    // Пункт задания: 1.3.
    /// <summary>
    /// Тестирование репозитория людей
    /// </summary>
    public class PersonRepositoryTests : IClassFixture<DatabaseFixture>
    {
        /// <summary>
        /// Fixture базы данных
        /// </summary>
        private readonly DatabaseFixture _fixture;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fixture">Fixture базы данных</param>
        public PersonRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetFilteredListAsync_WithExistFirstName_ShouldReturn_PersonByFirstName()
        {
            // Arrange
            const string expectedFirstName = "Пётр";
            await using var context = _fixture.CreateContext();
            var repository = new PersonRepository(context);

            // Act
            var people = await repository.GetFilteredListAsync(new PersonFilter { FirstName = expectedFirstName });

            // Assert
            Assert.NotEmpty(people);
            Assert.All(people, p => Assert.Equal(expectedFirstName, p.FirstName));
        }

        [Fact]
        public async Task GetFilteredListAsync_WithExistLastName_ShouldReturn_PersonByLastName()
        {
            // Arrange
            const string expectedLastName = "Петров";
            await using var context = _fixture.CreateContext();
            var repository = new PersonRepository(context);

            // Act
            var people = await repository.GetFilteredListAsync(new PersonFilter { LastName = expectedLastName });

            // Assert
            Assert.NotEmpty(people);
            Assert.All(people, p => Assert.Equal(expectedLastName, p.LastName));
        }

        [Fact]
        public async Task GetFilteredListAsync_WithExistMiddleName_ShouldReturn_PersonByMiddleName()
        {
            // Arrange
            const string expectedMiddleName = "Петрович";
            await using var context = _fixture.CreateContext();
            var repository = new PersonRepository(context);

            // Act
            var people = await repository.GetFilteredListAsync(new PersonFilter { MiddleName = expectedMiddleName });

            // Assert
            Assert.NotEmpty(people);
            Assert.All(people, p => Assert.Equal(expectedMiddleName, p.MiddleName));
        }

        [Fact]
        public async Task GetFilteredListAsync_WithNullFilter_ShouldReturn_NullReferenceException()
        {
            // Arrange
            await using var context = _fixture.CreateContext();
            var repository = new PersonRepository(context);

            // Act
            var result = await Record.ExceptionAsync(() => repository.GetFilteredListAsync(null));

            // Assert
            Assert.IsType<NullReferenceException>(result);
        }

        [Fact]
        public async Task GetLibraryCardsAsync_WithExistPersonId_ShouldReturn_LibraryCardsByPerson()
        {
            // Arrange
            await using var context = _fixture.CreateContext();
            var expectedCount = await context.LibraryCard
                                             .Where(lc => lc.PersonId == 1)
                                             .CountAsync();

            var repository = new PersonRepository(context);

            // Act
            var libraryCards = await repository.GetLibraryCardsAsync(1);

            // Assert
            Assert.Equal(expectedCount, libraryCards.Count);
            Assert.All(libraryCards, lc => Assert.Equal(1, lc.PersonId));
        }

        [Fact]
        public async Task ReceiveBookAsync_WithExistPersonId_WithExistPersonBookId_ShouldAddToDb_LibraryCard()
        {
            // Arrange
            await using var transaction = await _fixture.Connection.BeginTransactionAsync();

            await using (var context = _fixture.CreateContext(transaction))
            {
                var repository = new PersonRepository(context);

                // Act
                await repository.ReceiveBookAsync(1, 1);
            }

            // Assert
            await using (var context = _fixture.CreateContext(transaction))
            {
                var addedLibraryCards = await context.LibraryCard.FindAsync(1, 1);
                Assert.NotNull(addedLibraryCards);
            }
        }

        [Fact]
        public async Task ReturnBookAsync_WithExistLibraryCardPersonIdAndBookId_ShouldDeleteFromDb_LibraryCard()
        {
            // Arrange
            await using var transaction = await _fixture.Connection.BeginTransactionAsync();

            await using (var context = _fixture.CreateContext(transaction))
            {
                var repository = new PersonRepository(context);

                // Act
                await repository.ReturnBookAsync(1, 3);
            }

            // Assert
            await using (var context = _fixture.CreateContext(transaction))
            {
                var addedLibraryCards = await context.LibraryCard.FindAsync(1, 3);
                Assert.Null(addedLibraryCards);
            }
        }
    }
}
