using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterface;
using Moq;

namespace Application.Test.Services
{
    public class OrderServiceTest
    {
        [Fact]
        public async Task GetByIdAsync_WhenCalled_ReturnsOrder()
        {
            // Arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var orderService = new OrderService(repositoryMock.Object, mapperMock.Object);

            var orderId = 1;
            var orderEntity = new Order
            {
                OrderId = orderId,
                CustomerId = 1,
                OrderDate = DateTime.Now,
                TotalAmount = 100000,
                Paid = true,
                Creator = 1,
                DateCreated = DateTime.Now,
                Retired = false,
                Customer = new Customer
                {
                    CustomerId = 1,
                    GivenName = "David",
                    FamilyName = "Monsalve",
                    Email = "david.monsalve@gmail.com",
                    Phone = "3145850749",
                    Address = "Calle 80 # 95 A 118",
                    Creator = 1,
                    DateCreated = DateTime.Now,
                    Retired = false
                }
            };
            var orderDTO = new OrderDTO
            {
                OrderId = orderId,
                CustomerId = 1,
                OrderDate = DateTime.Now,
                TotalAmount = 100000,
                Paid = true,
                Customer = new CustomerDTO
                {
                    CustomerId = 1,
                    GivenName = "David",
                    FamilyName = "Monsalve",
                    Email = "david.monsalve@gmail.com",
                    Phone = "3145850749",
                    Address = "Calle 80 # 95 A 118"
                }
            };

            repositoryMock.Setup(x => x.GetByIdAsync(orderId)).ReturnsAsync(orderEntity);
            mapperMock.Setup(x => x.Map<Order, OrderDTO>(orderEntity)).Returns(orderDTO);

            // Act
            var result = await orderService.GetByIdAsync(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.IsNotType<Exception>(result);
            Assert.IsType<OrderDTO>(result);
            Assert.Equal(orderId, result.OrderId);
            Assert.Equal(orderEntity.CustomerId, result.CustomerId);
            Assert.Equal(orderEntity.TotalAmount, result.TotalAmount);
            Assert.Equal(orderEntity.Paid, result.Paid);
            Assert.Equal(orderEntity.CustomerId, result.Customer?.CustomerId);
        }

        [Fact]
        public async Task AddAsync_WhenCalled()
        {
            // Arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var orderService = new OrderService(repositoryMock.Object, mapperMock.Object);

            var orderDTO = new OrderDTO
            {
                CustomerId = 1,
                OrderDate = DateTime.Now,
                TotalAmount = 50000,
                Paid = true
            };
            var orderEntity = new Order
            {
                OrderId = 1,
                CustomerId = 1,
                OrderDate = DateTime.Now,
                TotalAmount = 50000,
                DateCreated = DateTime.Now,
                Creator = 1,
                Retired = false
            };

            mapperMock.Setup(x => x.Map<OrderDTO, Order>(orderDTO)).Returns(orderEntity);
            repositoryMock.Setup(x => x.AddAsync(orderEntity));

            // Act
            await orderService.AddAsync(orderDTO);

            // Assert
            repositoryMock.Verify(x => x.AddAsync(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled()
        {
            // Arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var orderService = new OrderService(repositoryMock.Object, mapperMock.Object);

            var orderId = 1;
            var orderDTO = new OrderDTO
            {
                OrderId = orderId,
                CustomerId = 1,
                OrderDate = DateTime.Now,
                TotalAmount = 250000,
                Paid = true
            };
            var orderEntity = new Order
            {
                OrderId = orderId,
                CustomerId = 1,
                OrderDate = DateTime.Now,
                TotalAmount = 250000,
                Paid = true,
                DateCreated = DateTime.Now,
                Creator = 1,
                Retired = false
            };

            repositoryMock.Setup(x => x.GetByIdAsync(orderId)).ReturnsAsync(orderEntity);
            mapperMock.Setup(x => x.Map(orderDTO, orderEntity)).Returns(orderEntity);
            repositoryMock.Setup(x => x.UpdateAsync(orderEntity));

            // Act
            await orderService.UpdateAsync(orderDTO);

            // Assert
            repositoryMock.Verify(x => x.GetByIdAsync(orderDTO.OrderId.Value), Times.Once);
            repositoryMock.Verify(x => x.UpdateAsync(orderEntity), Times.Once);
        }

        [Fact]
        public async Task DeleteByIdAsync_WhenCalled()
        {
            // Arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var orderService = new OrderService(repositoryMock.Object, mapperMock.Object);

            var orderId = 1;

            repositoryMock.Setup(x => x.DeleteByIdAsync(orderId));

            // Act
            await orderService.DeleteByIdAsync(orderId);

            // Assert
            repositoryMock.Verify(x => x.DeleteByIdAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task DeleteLogicallyByIdAsync_WhenCalled()
        {
            // Arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var orderService = new OrderService(repositoryMock.Object, mapperMock.Object);

            var orderId = 1;

            repositoryMock.Setup(x => x.DeleteLogicallyByIdAsync(orderId));

            // Act
            await orderService.DeleteLogicallyByIdAsync(orderId);

            // Assert
            repositoryMock.Verify(x => x.DeleteLogicallyByIdAsync(orderId), Times.Once);
        }
    }
}
