using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using SimbirHomeworkClean.Application.Contracts.Repositories;
using SimbirHomeworkClean.Application.DTOs.Genre;
using SimbirHomeworkClean.Application.Services;
using SimbirHomeworkClean.Domain.Entities;
using Xunit;

namespace SimbirHomeworkClean.Application.UnitTests.Services
{
    // Пункт задания: 1.3.
    /// <summary>
    /// Тестирование сервиса жанров
    /// </summary>
    public class GenreServiceTests
    {
        /// <summary>
        /// Маппер
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор
        /// </summary>
        public GenreServiceTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddMaps(typeof(Injection))));
        }

        [Fact]
        public async Task GetListAsync_ShouldReturn_Genres()
        {
            // Arrange
            var genre1 = new Genre { Id = 1, GenreName = "TestGenre1" };
            var genre2 = new Genre { Id = 2, GenreName = "TestGenre2" };
            var genres = new List<Genre> { genre1, genre2 };

            // Пункт задания: 1.4.
            var genreRepositoryMock = new Mock<IGenreRepository>();

            genreRepositoryMock.Setup(r => r.GetListAsync()).Returns(Task.FromResult(genres));

            var service = new GenreService(_mapper, genreRepositoryMock.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.Equal(genres.Count, result.Count());
        }

        [Fact]
        public async Task GetStatistic_ShouldReturn_GenreStatistic()
        {
            // Arrange
            var genre1 = new Genre { Id = 1, GenreName = "TestGenre1" };
            var genre2 = new Genre { Id = 2, GenreName = "TestGenre2" };
            var genreStatistic = new List<(Genre, int)> { (genre1, 5), (genre2, 4) };

            // Пункт задания: 1.4.
            var genreRepositoryMock = new Mock<IGenreRepository>();

            genreRepositoryMock.Setup(r => r.GetStatisticAsync()).Returns(Task.FromResult(genreStatistic));

            var service = new GenreService(_mapper, genreRepositoryMock.Object);

            // Act
            var result = await service.GetStatisticAsync();

            // Assert
            Assert.Equal(genreStatistic.Count, result.Count());
            Assert.Equal(genreStatistic[0].Item2, result.First(r => r.Genre.Id == genreStatistic[0].Item1.Id).BookCount);
            Assert.Equal(genreStatistic[1].Item2, result.First(r => r.Genre.Id == genreStatistic[1].Item1.Id).BookCount);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddAndReturn_Genre()
        {
            // Arrange
            var genreDto1 = new CreateGenreDto { GenreName = "TestGenre1" };
            var genres = new List<Genre>();

            // Пункт задания: 1.4.
            var genreRepositoryMock = new Mock<IGenreRepository>();

            genreRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Genre>()))
                               .Returns((Genre g) =>
                                {
                                    g.Id = 4;
                                    genres.Add(g);
                                    return Task.FromResult(g);
                                });

            var service = new GenreService(_mapper, genreRepositoryMock.Object);

            // Act
            var result = await service.CreateAsync(genreDto1);

            // Assert
            Assert.Equal(4, result.Id);
            Assert.Equal(genreDto1.GenreName, result.GenreName);
            Assert.Single(genres);
        }
    }
}
