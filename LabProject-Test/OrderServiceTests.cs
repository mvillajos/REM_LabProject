
using LabProject.Repositories.Interfaces;
using LabProject.Services;
using LabProject.Models.Sales;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject_Test
{
    public class OrderServiceTests
    {
    
        Order testOrder1 = new Order { Id=1, CustomerId=1, OrderDate=new DateTime(2024,1,28,9,0,0) };
        Order testOrder2 = new Order { Id=2, CustomerId=1, OrderDate = new DateTime(2024, 1, 28, 10, 30, 0) };

        [Fact]
        public async Task GetAll_Orders_Success()
        {
            // Arrange
            var orderRepoMock = new Mock<IOrderRepository>();

            orderRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Order>
            {
                testOrder1, testOrder2
            });
            var orderService = new OrderService(orderRepoMock.Object);

            // Act            
            var result = await orderService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

        }



        [Fact]
        public async Task GetById_Orders_Success()
        {
            // Arrange
            var orderRepoMock = new Mock<IOrderRepository>();

            orderRepoMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(testOrder1);
            var orderService = new OrderService(orderRepoMock.Object);

            // Act            
            var result = await orderService.GetByIdAsync(1);


            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);

        }


    }
}
