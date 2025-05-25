using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using TrainMaster.Application.ExtensionError;
using TrainMaster.Application.Services;
using TrainMaster.Domain.Entity;
using TrainMaster.Infrastracture.Repository.Interfaces;
using TrainMaster.Infrastracture.Repository.RepositoryUoW;

namespace TrainMaster.Test.Services
{
    public class DepartmentServiceTest
    {
        private readonly Mock<IRepositoryUoW> _repositoryUoWMock;
        private readonly DepartmentService _departmentService;

        public DepartmentServiceTest()
        {
            _repositoryUoWMock = new Mock<IRepositoryUoW>();
            _repositoryUoWMock.Setup(x => x.BeginTransaction()).Returns(Mock.Of<IDbContextTransaction>());
            _departmentService = new DepartmentService(_repositoryUoWMock.Object);
        }

        [Fact]
        public async Task Add_ShouldReturnSuccess_WhenDepartmentIsValidAndNameIsUnique()
        {
            // Arrange
            var department = new DepartmentEntity
            {
                Name = "TI",
                Description = "Departamento de Tecnologia"
            };

            var departmentRepositoryMock = new Mock<IDepartmentRepository>();

            departmentRepositoryMock.Setup(x => x.GetByName(department.Name))
                                    .ReturnsAsync((DepartmentEntity?)null);
            departmentRepositoryMock.Setup(x => x.Add(It.IsAny<DepartmentEntity>()))
                                    .ReturnsAsync(department);

            _repositoryUoWMock.Setup(x => x.DepartmentRepository).Returns(departmentRepositoryMock.Object);
            _repositoryUoWMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _departmentService.Add(department);

            // Assert
            Assert.True(result.Success);
            departmentRepositoryMock.Verify(x => x.GetByName(department.Name), Times.Once);
            departmentRepositoryMock.Verify(x => x.Add(It.IsAny<DepartmentEntity>()), Times.Once);
            _repositoryUoWMock.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Add_ShouldReturnError_WhenDepartmentNameAlreadyExists()
        {
            // Arrange
            var department = new DepartmentEntity
            {
                Name = "TI",
                Description = "Departamento duplicado"
            };

            var existingDepartment = new DepartmentEntity
            {
                Id = 99,
                Name = "TI"
            };

            var departmentRepositoryMock = new Mock<IDepartmentRepository>();

            departmentRepositoryMock.Setup(x => x.GetByName(department.Name))
                                    .ReturnsAsync(existingDepartment);

            _repositoryUoWMock.Setup(x => x.DepartmentRepository).Returns(departmentRepositoryMock.Object);

            // Act
            var result = await _departmentService.Add(department);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Department already exists with that name", result.Message);
            departmentRepositoryMock.Verify(x => x.GetByName(department.Name), Times.Once);
            departmentRepositoryMock.Verify(x => x.Add(It.IsAny<DepartmentEntity>()), Times.Never);
            _repositoryUoWMock.Verify(x => x.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task Delete_ShouldSetDepartmentAsActive_WhenDepartmentExists()
        {
            // Arrange
            int departmentId = 1;

            var existingDepartment = new DepartmentEntity
            {
                Id = departmentId,
                Name = "RH",
                IsActive = false
            };

            var departmentRepositoryMock = new Mock<IDepartmentRepository>();

            departmentRepositoryMock.Setup(x => x.GetById(departmentId))
                                    .ReturnsAsync(existingDepartment);

            _repositoryUoWMock.Setup(x => x.DepartmentRepository).Returns(departmentRepositoryMock.Object);
            _repositoryUoWMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            await _departmentService.Delete(departmentId);

            // Assert
            departmentRepositoryMock.Verify(x => x.GetById(departmentId), Times.Once);
            departmentRepositoryMock.Verify(x => x.Update(It.Is<DepartmentEntity>(d =>
                d.Id == departmentId && d.IsActive == true
            )), Times.Once);
            _repositoryUoWMock.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldThrowInvalidOperationException_WhenRepositoryThrowsException()
        {
            // Arrange
            int departmentId = 1;

            var departmentRepositoryMock = new Mock<IDepartmentRepository>();

            departmentRepositoryMock.Setup(x => x.GetById(departmentId))
                                    .ThrowsAsync(new Exception("Simulated error"));

            _repositoryUoWMock.Setup(x => x.DepartmentRepository).Returns(departmentRepositoryMock.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => _departmentService.Delete(departmentId));

            Assert.Equal("Error to delete a department.", exception.Message);

            departmentRepositoryMock.Verify(x => x.GetById(departmentId), Times.Once);
            departmentRepositoryMock.Verify(x => x.Update(It.IsAny<DepartmentEntity>()), Times.Never);
            _repositoryUoWMock.Verify(x => x.SaveAsync(), Times.Never);
        }
    }
}
