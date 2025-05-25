using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using TrainMaster.Application.ExtensionError;
using TrainMaster.Application.Services;
using TrainMaster.Domain.Entity;
using TrainMaster.Infrastracture.Repository.Interfaces;
using TrainMaster.Infrastracture.Repository.RepositoryUoW;
using Xunit;

namespace TrainMaster.Test.Services
{
    public class AddressServiceTest
    {
        private readonly Mock<IRepositoryUoW> _repositoryUoWMock;
        private readonly Mock<IAddressRepository> _addressRepositoryMock;
        private readonly AddressService _addressService;

        public AddressServiceTest()
        {
            _repositoryUoWMock = new Mock<IRepositoryUoW>();
            _addressRepositoryMock = new Mock<IAddressRepository>();

            _repositoryUoWMock.Setup(x => x.AddressRepository).Returns(_addressRepositoryMock.Object);
            _repositoryUoWMock.Setup(x => x.BeginTransaction()).Returns(Mock.Of<IDbContextTransaction>());

            _addressService = new AddressService(_repositoryUoWMock.Object);
        }

        [Fact]
        public async Task Add_ShouldReturnSuccess_WhenAddressIsValid()
        {
            // Arrange
            var address = new AddressEntity
            {
                PostalCode = "60170150",
                Street = "Rua Teste",
                Neighborhood = "Bairro Teste",
                City = "Cidade Teste",
                Uf = "CE"
            };

            _addressRepositoryMock.Setup(x => x.Add(It.IsAny<AddressEntity>())).ReturnsAsync(address);
            _repositoryUoWMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _addressService.Add(address);

            // Assert
            Assert.True(result.Success);
            _addressRepositoryMock.Verify(x => x.Add(It.IsAny<AddressEntity>()), Times.Once);
            _repositoryUoWMock.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Add_ShouldReturnError_WhenPostalCodeIsInvalid()
        {
            // Arrange
            var address = new AddressEntity
            {
                PostalCode = "123", // inválido
                Street = "Rua Teste",
                Neighborhood = "Bairro Teste",
                City = "Cidade Teste",
                Uf = "CE"
            };

            // Act
            var result = await _addressService.Add(address);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Postal Code invalid.", result.Message);
            _addressRepositoryMock.Verify(x => x.Add(It.IsAny<AddressEntity>()), Times.Never);
        }

        [Fact]
        public async Task GetById_ShouldReturnAddress_WhenExists()
        {
            // Arrange
            var address = new AddressEntity
            {
                Id = 1,
                PostalCode = "60170150"
            };

            _addressRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync(address);

            // Act
            var result = await _addressService.GetById(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(address, result.Data);
            _addressRepositoryMock.Verify(x => x.GetById(1), Times.Once);
        }

        [Fact]
        public async Task GetById_ShouldReturnError_WhenNotExists()
        {
            // Arrange
            _addressRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync((AddressEntity?)null);

            // Act
            var result = await _addressService.GetById(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Curso não encontrado", result.Message);
            _addressRepositoryMock.Verify(x => x.GetById(1), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnSuccess_WhenAddressExists()
        {
            // Arrange
            var existingAddress = new AddressEntity
            {
                Id = 1,
                PostalCode = "60170150",
                Street = "Antiga Rua"
            };

            var updateData = new AddressEntity
            {
                PostalCode = "60170150",
                Street = "Nova Rua",
                City = "Cidade",
                Uf = "CE",
                Neighborhood = "Bairro"
            };

            _addressRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync(existingAddress);
            _repositoryUoWMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _addressService.Update(1, updateData);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Nova Rua", existingAddress.Street);
            _repositoryUoWMock.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenAddressNotExists()
        {
            // Arrange
            _addressRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync((AddressEntity?)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _addressService.Update(1, new AddressEntity()));
            _addressRepositoryMock.Verify(x => x.Update(It.IsAny<AddressEntity>()), Times.Never);
        }

        [Fact]
        public async Task Delete_ShouldSetAsActive_WhenAddressExists()
        {
            // Arrange
            var address = new AddressEntity
            {
                Id = 1,
                PostalCode = "60170150"
            };

            _addressRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync(address);
            _repositoryUoWMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            await _addressService.Delete(1);

            // Assert
            _addressRepositoryMock.Verify(x => x.GetById(1), Times.Once);
            _addressRepositoryMock.Verify(x => x.Update(It.IsAny<AddressEntity>()), Times.Once);
            _repositoryUoWMock.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenExceptionOccurs()
        {
            // Arrange
            _addressRepositoryMock
                .Setup(x => x.GetById(1))
                .ThrowsAsync(new Exception("Erro simulado"));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _addressService.Delete(1));
            _repositoryUoWMock.Verify(x => x.SaveAsync(), Times.Never);
        }
    }
}
