using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using SimbirHomeworkClean.Application.Contracts.Repositories;
using SimbirHomeworkClean.Application.DTOs.Person;
using SimbirHomeworkClean.Application.Filters;
using SimbirHomeworkClean.Application.Services;
using SimbirHomeworkClean.Domain.Entities;
using Xunit;

namespace SimbirHomeworkClean.Application.UnitTests.Services
{
    // Пункт задания: 1.3.
    /// <summary>
    /// Тестирование сервиса людей
    /// </summary>
    public class PersonServiceTests
    {
        /// <summary>
        /// Маппер
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор
        /// </summary>
        public PersonServiceTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddMaps(typeof(Injection))));
        }

        [Fact]
        public async Task CreateAsync_ShouldAddAndReturn_Person()
        {
            // Arrange
            var personDto = new CreatePersonDto { FirstName = "TestFirstName", LastName = "TestLastName", MiddleName = "TestMiddleName", BirthDate = new DateTime(2020, 01, 01) };
            var people = new List<Person>();

            // Пункт задания: 1.4.
            var personRepositoryMock = new Mock<IPersonRepository>();

            personRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Person>()))
                                .Returns((Person g) =>
                                 {
                                     g.Id = 4;
                                     people.Add(g);
                                     return Task.FromResult(g);
                                 });

            var service = new PersonService(_mapper, personRepositoryMock.Object);

            // Act
            var result = await service.CreateAsync(personDto);

            // Assert
            Assert.Equal(4, result.Id);
            Assert.Equal(personDto.FirstName, result.FirstName);
            Assert.Equal(personDto.LastName, result.LastName);
            Assert.Equal(personDto.MiddleName, result.MiddleName);
            Assert.Equal(personDto.BirthDate, result.BirthDate);
            Assert.Single(people);
        }

        [Fact]
        public async Task DeleteAsync_WithExistId_ShouldDelete_Person()
        {
            // Arrange
            var people = new List<Person>
            {
                new Person { Id = 22, FirstName = "TestFirstName1", LastName = "TestLastName1", MiddleName = "TestMiddleName1", BirthDate = new DateTime(2020, 01, 01) }
            };

            // Пункт задания: 1.4.
            var personRepositoryMock = new Mock<IPersonRepository>();

            personRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Person>()))
                                .Returns((Person p) =>
                                 {
                                     people.Remove(p);
                                     return Task.CompletedTask;
                                 });

            personRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                                .Returns((int id) => { return Task.FromResult(people.Single(p => p.Id == id)); });

            var service = new PersonService(_mapper, personRepositoryMock.Object);

            // Act
            await service.DeleteAsync(22);

            // Assert
            Assert.Empty(people);
        }

        [Fact]
        public async Task UpdateAsync_WithExistId_ShouldUpdateAndReturn_Person()
        {
            // Arrange
            var createPersonDto = new CreatePersonDto { FirstName = "TestFirstName2", LastName = "TestLastName2", MiddleName = "TestMiddleName2", BirthDate = new DateTime(2012, 02, 02) };
            var people = new List<Person>
            {
                new Person { Id = 22, FirstName = "TestFirstName", LastName = "TestLastName", MiddleName = "TestMiddleName", BirthDate = new DateTime(2020, 01, 01) }
            };

            // Пункт задания: 1.4.
            var personRepositoryMock = new Mock<IPersonRepository>();

            personRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Person>()))
                                .Returns((Person p) =>
                                 {
                                     people[0] = p;
                                     return Task.FromResult(p);
                                 });

            personRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                                .Returns((int id) => { return Task.FromResult(people.Single(p => p.Id == id)); });

            var service = new PersonService(_mapper, personRepositoryMock.Object);

            // Act
            var result = await service.UpdateAsync(22, createPersonDto);

            // Assert
            Assert.Equal(22, result.Id);
            Assert.Equal(createPersonDto.FirstName, result.FirstName);
            Assert.Equal(createPersonDto.LastName, result.LastName);
            Assert.Equal(createPersonDto.MiddleName, result.MiddleName);
            Assert.Equal(createPersonDto.BirthDate, result.BirthDate);
            Assert.Single(people);
        }

        [Fact]
        public async Task DeleteByFullNameAsync_WithExistFullName_ShouldDelete_PeopleByFullname()
        {
            // Arrange
            var people = new List<Person>
            {
                new Person { Id = 21, FirstName = "TestFirstName1", LastName = "TestLastName1", MiddleName = "TestMiddleName1", BirthDate = new DateTime(2020, 01, 01) },
                new Person { Id = 22, FirstName = "TestFirstName1", LastName = "TestLastName1", MiddleName = "TestMiddleName1", BirthDate = new DateTime(2020, 01, 01) },
                new Person { Id = 23, FirstName = "TestFirstName2", LastName = "TestLastName2", MiddleName = "TestMiddleName2", BirthDate = new DateTime(2020, 01, 01) }
            };

            // Пункт задания: 1.4.
            var personRepositoryMock = new Mock<IPersonRepository>();

            personRepositoryMock.Setup(r => r.DeleteRangeAsync(It.IsAny<IEnumerable<Person>>()))
                                .Returns((IEnumerable<Person> peop) =>
                                 {
                                     people = people.Where(p => !peop.Contains(p)).ToList();
                                     return Task.CompletedTask;
                                 });

            personRepositoryMock.Setup(r => r.GetFilteredListAsync(It.IsAny<PersonFilter>()))
                                .Returns((PersonFilter f) =>
                                 {
                                     return Task.FromResult(people.Where(p => p.FirstName == f.FirstName && p.LastName == f.LastName && p.MiddleName == f.MiddleName)
                                                                  .ToList());
                                 });

            var service = new PersonService(_mapper, personRepositoryMock.Object);

            // Act
            await service.DeleteByFullNameAsync(new DeletePersonByFullNameCommand { FirstName = "TestFirstName1", LastName = "TestLastName1", MiddleName = "TestMiddleName1" });

            // Assert
            Assert.Single(personRepositoryMock.Invocations.Where(x => x.Method.Name == "DeleteRangeAsync"));
            Assert.Single(personRepositoryMock.Invocations.Where(x => x.Method.Name == "GetFilteredListAsync"));
            Assert.Single(people);
            Assert.All(people, p => Assert.NotEqual("TestFirstName1", p.FirstName));
            Assert.All(people, p => Assert.NotEqual("TestLastName1", p.LastName));
            Assert.All(people, p => Assert.NotEqual("TestMiddleName1", p.MiddleName));
        }

        [Fact]
        public async Task GetPersonLibraryCardsAsync_WithExistId_ShouldReturn_ExpectedLibraryCardsCount()
        {
            // Arrange
            var libraryCards = new List<LibraryCard>
            {
                new LibraryCard { PersonId = 13, BookId = 666 },
                new LibraryCard { PersonId = 13, BookId = 666 },
                new LibraryCard { PersonId = 14, BookId = 777 }
            };

            // Пункт задания: 1.4.
            var personRepositoryMock = new Mock<IPersonRepository>();

            personRepositoryMock.Setup(r => r.GetLibraryCardsAsync(It.IsAny<int>()))
                                .Returns((int id) => Task.FromResult(libraryCards.Where(lc => lc.PersonId == id).ToList()));

            var service = new PersonService(_mapper, personRepositoryMock.Object);

            // Act
            var result = await service.GetPersonLibraryCardsAsync(13);

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task ReceiveBookAsync_WithExistId_ShouldReturn_PersonWithLibraryCards()
        {
            // Arrange
            var people = new List<Person>
            {
                new Person
                {
                    Id = 22,
                    FirstName = "TestFirstName",
                    LastName = "TestLastName",
                    MiddleName = "TestMiddleName",
                    BirthDate = new DateTime(2020, 01, 01)
                }
            };

            // Пункт задания: 1.4.
            var personRepositoryMock = new Mock<IPersonRepository>();

            personRepositoryMock.Setup(r => r.ReceiveBookAsync(It.IsAny<int>(), It.IsAny<int>()))
                                .Returns((int id, int bId) => Task.FromResult(new Person
                                 {
                                     Id = id,
                                     FirstName = "TestFirstName",
                                     LastName = "TestLastName",
                                     MiddleName = "TestMiddleName",
                                     BirthDate = new DateTime(2012, 02, 02),
                                     LibraryCards = new[]
                                     {
                                         new LibraryCard
                                         {
                                             PersonId = id,
                                             BookId = bId
                                         }
                                     }
                                 }));

            personRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                                .Returns((int id) => { return Task.FromResult(people.Single(p => p.Id == id)); });

            var service = new PersonService(_mapper, personRepositoryMock.Object);

            // Act
            var result = await service.ReceiveBookAsync(22, 11);

            // Assert
            Assert.Equal(22, result.Id);
            Assert.Single(result.LibraryCards);
        }

        [Fact]
        public async Task ReturnBookAsync_WithExistId_ShouldReturn_PersonWithLibraryCards()
        {
            // Arrange
            var people = new List<Person>
            {
                new Person
                {
                    Id = 22,
                    FirstName = "TestFirstName",
                    LastName = "TestLastName",
                    MiddleName = "TestMiddleName",
                    BirthDate = new DateTime(2020, 01, 01),
                    LibraryCards = new[]
                    {
                        new LibraryCard
                        {
                            PersonId = 22,
                            BookId = 11
                        }
                    }
                }
            };

            // Пункт задания: 1.4.
            var personRepositoryMock = new Mock<IPersonRepository>();

            personRepositoryMock.Setup(r => r.ReturnBookAsync(It.IsAny<int>(), It.IsAny<int>()))
                                .Returns((int id, int bId) => Task.FromResult(new Person
                                 {
                                     Id = id,
                                     FirstName = "TestFirstName",
                                     LastName = "TestLastName",
                                     MiddleName = "TestMiddleName",
                                     BirthDate = new DateTime(2012, 02, 02),
                                     LibraryCards = Array.Empty<LibraryCard>()
                                 }));

            personRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                                .Returns((int id) => { return Task.FromResult(people.Single(p => p.Id == id)); });

            var service = new PersonService(_mapper, personRepositoryMock.Object);

            // Act
            var result = await service.ReturnBookAsync(22, 11);

            // Assert
            Assert.Equal(22, result.Id);
            Assert.Empty(result.LibraryCards);
        }
    }
}
