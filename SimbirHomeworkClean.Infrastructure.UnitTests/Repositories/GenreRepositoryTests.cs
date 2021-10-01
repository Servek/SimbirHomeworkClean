using System;
using System.Linq;
using System.Threading.Tasks;
using SimbirHomeworkClean.Domain.Entities;
using SimbirHomeworkClean.Infrastructure.Repositories;
using SimbirHomeworkClean.Infrastructure.UnitTests.Fixtures;
using Xunit;

namespace SimbirHomeworkClean.Infrastructure.UnitTests.Repositories
{
    // Пункт задания: 1.3.
    /// <summary>
    /// Тестирование репозитория жанров
    /// </summary>
    public class GenreRepositoryTests : IClassFixture<DatabaseFixture>
    {
        /// <summary>
        /// Fixture базы данных
        /// </summary>
        private readonly DatabaseFixture _fixture;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fixture">Fixture базы данных</param>
        public GenreRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        #region From base repository // Тестирование методов из базового репозитория
        [Fact]
        public async Task GetListAsync_ShouldReturn_Genres()
        {
            // Arrange
            await using var context = _fixture.CreateContext();
            var expectedCount = 3;
            var repository = new GenreRepository(context);

            // Act
            var genres = await repository.GetListAsync();

            // Assert
            Assert.Equal(expectedCount, genres.Count);
        }

        [Fact]
        public async Task GetByIdAsync_WithExistGenreId_ShouldReturn_Genre()
        {
            // Arrange
            await using var context = _fixture.CreateContext();
            var expectedGenre = new Genre
            {
                Id = 1,
                GenreName = "Роман"
            };
            var repository = new GenreRepository(context);

            // Act
            var genre = await repository.GetByIdAsync(1);

            // Assert
            Assert.Equal(expectedGenre.Id, genre.Id);
            Assert.Equal(expectedGenre.GenreName, genre.GenreName);
        }

        [Fact]
        public async Task AddAsync_ShouldAddToDb_Genre()
        {
            // Arrange
            var expectedGenre = new Genre { GenreName = "TestGenre" };
            await using var transaction = await _fixture.Connection.BeginTransactionAsync();

            await using (var context = _fixture.CreateContext(transaction))
            {
                var repository = new GenreRepository(context);

                // Act
                await repository.AddAsync(expectedGenre);
            }

            // Assert
            await using (var context = _fixture.CreateContext(transaction))
            {
                var addedGenre = context.Genre.SingleOrDefault(g => g.GenreName == expectedGenre.GenreName);
                Assert.NotNull(addedGenre);
            }
        }

        [Fact]
        public async Task AddRangeAsync_ShouldAddToDb_GenresCollection()
        {
            // Arrange
            var expectedGenres = new[]
            {
                new Genre { GenreName = "TestGenre1" },
                new Genre { GenreName = "TestGenre2" }
            };

            await using var transaction = await _fixture.Connection.BeginTransactionAsync();

            await using (var context = _fixture.CreateContext(transaction))
            {
                var repository = new GenreRepository(context);

                // Act
                await repository.AddRangeAsync(expectedGenres);
            }

            // Assert
            await using (var context = _fixture.CreateContext(transaction))
            {
                var addedGenre1 = context.Genre.SingleOrDefault(g => g.GenreName == expectedGenres[0].GenreName);
                var addedGenre2 = context.Genre.SingleOrDefault(g => g.GenreName == expectedGenres[1].GenreName);

                Assert.NotNull(addedGenre1);
                Assert.NotNull(addedGenre2);
            }
        }

        [Fact]
        public async Task UpdateAsync_WithExistGenreId_ShouldUpdateInDb_Genre()
        {
            // Arrange
            const string expectedGenreName = "UpdatedGenre";
            await using var transaction = await _fixture.Connection.BeginTransactionAsync();

            await using (var context = _fixture.CreateContext(transaction))
            {
                var repository = new GenreRepository(context);
                var updatingGenre = await context.Genre.FindAsync(1);
                updatingGenre.GenreName = expectedGenreName;

                // Act
                await repository.UpdateAsync(updatingGenre);
            }

            // Assert
            await using (var context = _fixture.CreateContext(transaction))
            {
                var updatedGenre = await context.Genre.FindAsync(1);
                Assert.Equal(expectedGenreName, updatedGenre.GenreName);
            }
        }

        [Fact]
        public async Task DeleteAsync_WithExistGenreId_ShouldDeleteFromDb_Genre()
        {
            // Arrange
            await using var transaction = await _fixture.Connection.BeginTransactionAsync();

            await using (var context = _fixture.CreateContext(transaction))
            {
                var repository = new GenreRepository(context);
                var deletingGenre = await context.Genre.FindAsync(1);

                // Act
                await repository.DeleteAsync(deletingGenre);
            }

            // Assert
            await using (var context = _fixture.CreateContext(transaction))
            {
                var deletedGenre = await context.Genre.FindAsync(1);
                Assert.Null(deletedGenre);
            }
        }

        [Fact]
        public async Task DeleteRangeAsync_WithExistGenresIds_ShouldDeleteFromDb_Genres()
        {
            // Arrange
            await using var transaction = await _fixture.Connection.BeginTransactionAsync();

            await using (var context = _fixture.CreateContext(transaction))
            {
                var repository = new GenreRepository(context);
                var deletingGenre1 = await context.Genre.FindAsync(1);
                var deletingGenre2 = await context.Genre.FindAsync(2);

                // Act
                await repository.DeleteRangeAsync(new[] { deletingGenre1, deletingGenre2 });
            }

            // Assert
            await using (var context = _fixture.CreateContext(transaction))
            {
                var deletedGenre1 = await context.Genre.FindAsync(1);
                var deletedGenre2 = await context.Genre.FindAsync(2);

                Assert.Null(deletedGenre1);
                Assert.Null(deletedGenre2);
            }
        }
        #endregion

        [Fact]
        public async Task GetListByGenreNamesAsync_WithExistGenresNames_ShouldReturn_GenresByNames()
        {
            // Arrange
            await using var context = _fixture.CreateContext();
            var expectedGenres = new [] { "Роман", "Фантастика"};
            var repository = new GenreRepository(context);

            // Act
            var genres = await repository.GetListByGenreNamesAsync(new[] { expectedGenres[0], expectedGenres[1] });

            // Assert
            Assert.Contains(expectedGenres[0], genres.Select(g => g.GenreName));
            Assert.Contains(expectedGenres[1], genres.Select(g => g.GenreName));
            Assert.DoesNotContain("Детектив", genres.Select(g => g.GenreName));
        }

        [Fact]
        public async Task GetStatisticAsync_ShouldReturn_GenresStatisticTuples()
        {
            // Arrange
            await using var context = _fixture.CreateContext();
            var expectedStatistic = context.Genre
                                           .Select(g => new Tuple<Genre, int>(g, g.Books.Count).ToValueTuple())
                                           .ToList();

            var repository = new GenreRepository(context);

            // Act
            var statistic = await repository.GetStatisticAsync();

            // Assert
            Assert.Equal(3, statistic.Count);
        }
    }
}
