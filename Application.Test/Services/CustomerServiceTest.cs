using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterface;
using Moq;

namespace Application.Test.Services
{
    public class CustomerServiceTest
    {
        [Fact]
        public async Task GetByIdAsync_WhenCalled_ReturnsCustomer()
        {
            // Arrange
            var repositoryMock = new Mock<ICustomerRepository>();
            var mapperMock = new Mock<IMapper>();

            var customerService = new CustomerService(repositoryMock.Object, mapperMock.Object);

            var customerId = 1;
            var customerEntity = new Customer
            {
                CustomerId = customerId,
                GivenName = "David",
                FamilyName = "Monsalve",
                Email = "david.monsalve@gmail.com",
                Phone = "3145850749",
                Address = "Calle 80 # 95 A 118",
                DateCreated = DateTime.Now,
                Creator = 1,
                Retired = false
            };
            var customerDTO = new CustomerDTO
            {
                CustomerId = 1,
                GivenName = "David",
                FamilyName = "Monsalve",
                Email = "david.monsalve@gmail.com",
                Phone = "3145850749",
                Address = "Calle 80 # 95 A 118"
            };

            repositoryMock.Setup(x => x.GetByIdAsync(customerId)).ReturnsAsync(customerEntity);
            mapperMock.Setup(x => x.Map<Customer, CustomerDTO>(customerEntity)).Returns(customerDTO);

            // Act
            var result = await customerService.GetByIdAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerDTO.CustomerId, result.CustomerId);
            Assert.Equal(customerDTO.GivenName, result.GivenName);
            Assert.Equal(customerDTO.FamilyName, result.FamilyName);
            Assert.Equal(customerDTO.Email, result.Email);
            Assert.Equal(customerDTO.Phone, result.Phone);
            Assert.Equal(customerDTO.Address, result.Address);
            Assert.IsNotType<Exception>(result);
            Assert.IsType<CustomerDTO>(result);

        }

        [Fact]
        public async Task AddAsync_WhenCalled()
        {
            // Arrange
            var repositoryMock = new Mock<ICustomerRepository>();
            var mapperMock = new Mock<IMapper>();

            var customerService = new CustomerService(repositoryMock.Object, mapperMock.Object);

            var customerDTO = new CustomerDTO
            {
                GivenName = "David",
                FamilyName = "Monsalve",
                Email = "david.monsalve@gmail.com",
                Phone = "3145850749",
                Address = "Calle 80 # 95 A 118"
            };
            var customerEntity = new Customer
            {
                CustomerId = 1,
                GivenName = "David",
                FamilyName = "Monsalve",
                Email = "david.monsalve@gmail.com",
                Phone = "3145850749",
                Address = "Calle 80 # 95 A 118"
            };

            mapperMock.Setup(x => x.Map<CustomerDTO, Customer>(customerDTO)).Returns(customerEntity);
            repositoryMock.Setup(x => x.AddAsync(customerEntity));

            // Act
            await customerService.AddAsync(customerDTO);

            // Assert
            repositoryMock.Verify(x => x.AddAsync(customerEntity), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled()
        {
            // Arrange
            var repositoryMock = new Mock<ICustomerRepository>();
            var mapperMock = new Mock<IMapper>();

            var customerService = new CustomerService(repositoryMock.Object, mapperMock.Object);

            var customerId = 1;
            var customerDTO = new CustomerDTO
            {
                CustomerId = customerId,
                GivenName = "David",
                FamilyName = "Monsalve",
                Email = "david.monsalve@gmail.com",
                Phone = "3145850749",
                Address = "Calle 80 # 95 A 118"
            };
            var customerEntity = new Customer
            {
                CustomerId = customerId,
                GivenName = "David",
                FamilyName = "Monsalve",
                Email = "david.monsalve@gmail.com",
                Phone = "3145850749",
                Address = "Calle 80 # 95 A 118"
            };

            repositoryMock.Setup(x => x.GetByIdAsync(customerDTO.CustomerId.Value)).ReturnsAsync(customerEntity);
            mapperMock.Setup(x => x.Map(customerDTO, customerEntity)).Returns(customerEntity);
            repositoryMock.Setup(x => x.UpdateAsync(customerEntity));

            // Act
            await customerService.UpdateAsync(customerDTO);

            // Assert
            repositoryMock.Verify(x => x.GetByIdAsync(customerDTO.CustomerId.Value), Times.Once);
            repositoryMock.Verify(x => x.UpdateAsync(customerEntity), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled_ThrowsException()
        {
            // Arrange
            var repositoryMock = new Mock<ICustomerRepository>();
            var mapperMock = new Mock<IMapper>();

            var customerService = new CustomerService(repositoryMock.Object, mapperMock.Object);

            var customerId = 1;
            var customerDTO = new CustomerDTO
            {
                CustomerId = customerId,
                GivenName = "David",
                FamilyName = "Monsalve",
                Email = "david.monsalve@gmail.com",
                Phone = "3145850749",
                Address = "Calle 80 # 95 A 118"
            };

            repositoryMock.Setup(x => x.GetByIdAsync(customerDTO.CustomerId.Value)).ReturnsAsync((Customer)null);

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => customerService.UpdateAsync(customerDTO));

            // Assert
            Assert.IsType<Exception>(exception);
            Assert.Equal("Customer not found", exception.Message);
        }

        [Fact]
        public async Task DeleteByIdAsync_WhenCalled()
        {
            // Arrange
            var repositoryMock = new Mock<ICustomerRepository>();
            var mapperMock = new Mock<IMapper>();

            var customerService = new CustomerService(repositoryMock.Object, mapperMock.Object);

            var customerId = 1;

            repositoryMock.Setup(x => x.DeleteByIdAsync(customerId));

            // Act
            await customerService.DeleteByIdAsync(customerId);

            // Assert
            repositoryMock.Verify(x => x.DeleteByIdAsync(customerId), Times.Once);
        }

        [Fact]
        public async Task DeleteLogicallyByIdAsync_WhenCalled()
        {
            // Arrange
            var repositoryMock = new Mock<ICustomerRepository>();
            var mapperMock = new Mock<IMapper>();

            var customerService = new CustomerService(repositoryMock.Object, mapperMock.Object);

            var customerId = 1;

            repositoryMock.Setup(x => x.DeleteLogicallyByIdAsync(customerId));

            // Act
            await customerService.DeleteLogicallyByIdAsync(customerId);

            // Assert
            repositoryMock.Verify(x => x.DeleteLogicallyByIdAsync(customerId), Times.Once);
        }
    }
}

