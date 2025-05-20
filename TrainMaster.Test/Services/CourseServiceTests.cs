using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using TrainMaster.Application.Services;
using TrainMaster.Domain.Entity;
using TrainMaster.Infrastracture.Repository.Interfaces;
using TrainMaster.Infrastracture.Repository.RepositoryUoW;

namespace TrainMaster.Test.Services
{
    public class CourseServiceTests
    {
        private readonly Mock<IRepositoryUoW> _repositoryUoWMock;
        private readonly Mock<ICourseRepository> _courseRepositoryMock;
        private readonly CourseService _courseService;

        public CourseServiceTests()
        {
            _repositoryUoWMock = new Mock<IRepositoryUoW>();
            _courseRepositoryMock = new Mock<ICourseRepository>();

            _repositoryUoWMock.Setup(x => x.CourseRepository).Returns(_courseRepositoryMock.Object);
            _repositoryUoWMock.Setup(x => x.BeginTransaction()).Returns(Mock.Of<IDbContextTransaction>());

            _courseService = new CourseService(_repositoryUoWMock.Object);
        }

        //[Fact]
        //public async Task Add_ShouldReturnSuccess_WhenCourseIsValid()
        //{
        //    // Arrange
        //    var course = new CourseEntity
        //    {
        //        Name = "Curso de Teste",
        //        Description = "Descrição",
        //        StartDate = DateTime.UtcNow,
        //        EndDate = DateTime.UtcNow.AddDays(30),
        //        UserId = 1
        //    };

        //    _courseRepositoryMock.Setup(x => x.Add(It.IsAny<CourseEntity>())).ReturnsAsync(course);

        //    // Act
        //    var result = await _courseService.Add(course);

        //    // Assert
        //    Assert.True(result.Success);
        //    _courseRepositoryMock.Verify(x => x.Add(It.IsAny<CourseEntity>()), Times.Once);
        //}

        //[Fact]
        //public async Task Add_ShouldReturnError_WhenEndDateIsBeforeStartDate()
        //{
        //    var course = new CourseEntity
        //    {
        //        Name = "Curso Inválido",
        //        Description = "Data inválida",
        //        StartDate = DateTime.UtcNow.AddDays(10),
        //        EndDate = DateTime.UtcNow.AddDays(5),
        //        UserId = 1
        //    };

        //    // Act
        //    var result = await _courseService.Add(course);

        //    // Assert
        //    Assert.False(result.Success);
        //    Assert.Equal("End date cannot be earlier than start date.", result.Message);
        //}

        [Fact]
        public async Task Get_ShouldReturnListOfCourses_WhenCoursesExist()
        {
            // Arrange
            var courses = new List<CourseEntity>
            {
                new CourseEntity { Id = 1, Name = "Curso 1", IsActive = true },
                new CourseEntity { Id = 2, Name = "Curso 2", IsActive = false }
            };

            _courseRepositoryMock.Setup(x => x.Get()).ReturnsAsync(courses);

            // Act
            var result = await _courseService.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Delete_ShouldDeactivateCourse_WhenCourseExists()
        {
            // Arrange
            var course = new CourseEntity { Id = 1, IsActive = true };
            _courseRepositoryMock.Setup(x => x.GetById(course.Id)).ReturnsAsync(course);
            _courseRepositoryMock.Setup(x => x.Update(It.IsAny<CourseEntity>()));

            // Act
            await _courseService.Delete(course.Id);

            // Assert
            _courseRepositoryMock.Verify(x => x.GetById(course.Id), Times.Once);
            _courseRepositoryMock.Verify(x => x.Update(It.Is<CourseEntity>(c => c.IsActive == true)), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnSuccess_WhenCourseIsUpdated()
        {
            // Arrange
            var course = new CourseEntity
            {
                Id = 1,
                Name = "Antigo",
                Description = "Antigo"
            };

            _courseRepositoryMock.Setup(x => x.GetById(course.Id)).ReturnsAsync(course);
            _courseRepositoryMock.Setup(x => x.Update(It.IsAny<CourseEntity>()));

            // Act
            course.Name = "Novo Curso";
            course.Description = "Nova descrição";
            var result = await _courseService.Update(course);

            // Assert
            Assert.True(result.Success);
            _courseRepositoryMock.Verify(x => x.Update(It.IsAny<CourseEntity>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenCourseNotFound()
        {
            _courseRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((CourseEntity)null!);

            var course = new CourseEntity { Id = 999 };

            await Assert.ThrowsAsync<InvalidOperationException>(() => _courseService.Update(course));
        }
    }
}