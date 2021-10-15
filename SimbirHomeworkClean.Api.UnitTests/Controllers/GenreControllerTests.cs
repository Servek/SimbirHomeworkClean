using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimbirHomeworkClean.Api.Controllers;
using SimbirHomeworkClean.Application.Contracts.Services;
using SimbirHomeworkClean.Application.DTOs.Genre;
using Xunit;

namespace SimbirHomeworkClean.Api.UnitTests.Controllers
{
    // Пункт задания: 1.3.
    /// <summary>
    /// Тестирование API контроллера жанров
    /// </summary>
    public class GenreControllerTests
    {
        [Fact]
        public async Task Get_ShouldReturn_ExpectedGenreCount()
        {
            // Arrange
            var genre1 = new GenreDto { Id = 1, GenreName = "TestGenre1" };
            var genre2 = new GenreDto { Id = 2, GenreName = "TestGenre2" };
            var genres = new[] { genre1, genre2 }.AsEnumerable();

            // Пункт задания: 1.4.
            var genreServiceMock = new Mock<IGenreService>();

            genreServiceMock.Setup(r => r.GetAllAsync()).Returns(Task.FromResult(genres));

            var controller = new GenreController(genreServiceMock.Object);

            // Act
            var result = (IEnumerable<GenreDto>)((OkObjectResult)await controller.Get()).Value;

            // Assert
            Assert.Equal(genres.Count(), result.Count());
        }

        [Fact]
        public async Task Statistic_ShouldReturn_ExpectedGenreStatisticCount()
        {
            // Arrange
            var genre1 = new GenreDto { Id = 1, GenreName = "TestGenre1" };
            var genre2 = new GenreDto { Id = 2, GenreName = "TestGenre2" };
            var genreStatistic1 = new GenreStatisticDto { Genre = genre1, BookCount = 6 };
            var genreStatistic2 = new GenreStatisticDto { Genre = genre2, BookCount = 8 };
            var genresStatistic = new[] { genreStatistic1, genreStatistic2 }.AsEnumerable();

            // Пункт задания: 1.4.
            var genreServiceMock = new Mock<IGenreService>();

            genreServiceMock.Setup(r => r.GetStatisticAsync()).Returns(Task.FromResult(genresStatistic));

            var controller = new GenreController(genreServiceMock.Object);

            // Act
            var result = (IEnumerable<GenreStatisticDto>)((OkObjectResult)await controller.Statistic()).Value;

            // Assert
            Assert.Equal(genresStatistic.Count(), result.Count());
        }

        [Fact]
        public async Task Post_ShouldReturn_Genre()
        {
            // Arrange
            var genre1 = new CreateGenreDto { GenreName = "TestGenre1" };

            // Пункт задания: 1.4.
            var genreServiceMock = new Mock<IGenreService>();

            genreServiceMock.Setup(r => r.CreateAsync(It.IsAny<CreateGenreDto>()))
                               .Returns((CreateGenreDto g) => Task.FromResult(new GenreDto
                                {
                                    Id = 5,
                                    GenreName = g.GenreName
                                }));

            var controller = new GenreController(genreServiceMock.Object);

            // Act
            var result = (GenreDto)((ObjectResult)await controller.Post(genre1)).Value;

            // Assert
            Assert.Equal(5, result.Id);
            Assert.Equal(genre1.GenreName, result.GenreName);
        }
    }
}
