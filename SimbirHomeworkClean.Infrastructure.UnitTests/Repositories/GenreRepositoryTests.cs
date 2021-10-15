using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SimbirHomeworkClean.Domain.Entities;
using SimbirHomeworkClean.Infrastructure.Repositories;
using SimbirHomeworkClean.Infrastructure.UnitTests.Extensions;
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

        #region From base repository
        // Тестирование методов из базового репозитория
        [Fact]
        public async Task GetListAsync_ShouldReturn_Genres()
        {
            // Arrange
            await using var context = _fixture.CreateContext();
            var expected = await context.Genre.ToListAsync();
            var repository = new GenreRepository(context);

            // Act
            var genres = await repository.GetListAsync();

            // Assert
            // Так как беру expected из контекста, то ExcludingAuditableFields не нужен
            // Но если подготавливать expected вручную, то надо исключить такие поля, как Created/Updated/RowVersion
            genres.Should().BeEquivalentTo(expected, opt => opt.ExcludingAuditingMembers());
        }

        [Fact]
        public async Task GetByIdAsync_WithExistGenreId_ShouldReturn_Genre()
        {
            // Arrange
            await using var context = _fixture.CreateContext();
            var expected = await context.Genre.FindAsync(1);
            var repository = new GenreRepository(context);

            // Act
            var genre = await repository.GetByIdAsync(1);

            // Assert
            genre.Should().BeEquivalentTo(expected);
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
                var final = await context.Genre.ToArrayAsync();
                final.Should().Contain(g => g.GenreName == expectedGenre.GenreName);
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
                var final = await context.Genre.ToArrayAsync();
                final.Should().Contain(g => g.GenreName == expectedGenres[0].GenreName);
                final.Should().Contain(g => g.GenreName == expectedGenres[1].GenreName);
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
                (await context.Genre.FindAsync(1)).GenreName.Should().Be(expectedGenreName);
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
                (await context.Genre.FindAsync(1)).Should().BeNull();
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
                (await context.Genre.FindAsync(1)).Should().BeNull();
                (await context.Genre.FindAsync(2)).Should().BeNull();
            }
        }
        #endregion

        [Fact]
        public async Task GetListByGenreNamesAsync_WithExistGenresNames_ShouldReturn_GenresByNames()
        {
            // Arrange
            await using var context = _fixture.CreateContext();
            var expected = new[] { "Роман", "Фантастика" };
            var repository = new GenreRepository(context);

            // Act
            var genres = await repository.GetListByGenreNamesAsync(new[] { expected[0], expected[1] });

            // Assert
            genres.Should().Contain(g => g.GenreName == expected[0]);
            genres.Should().Contain(g => g.GenreName == expected[1]);
            genres.Should().NotContain(g => g.GenreName == "Детектив");
        }

        [Fact]
        public async Task GetStatisticAsync_ShouldReturn_GenresStatisticTuples()
        {
            // Arrange
            await using var context = _fixture.CreateContext();
            var expected = await context.Genre
                                        .Select(g => new Tuple<Genre, int>(g, g.Books.Count).ToValueTuple())
                                        .ToListAsync();

            var repository = new GenreRepository(context);

            // Act
            var statistic = await repository.GetStatisticAsync();

            // Assert
            statistic.Should().BeEquivalentTo(expected);
        }
    }
}
