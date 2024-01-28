using LabProject.Models.Sales;
using LabProject.Repositories.Interfaces;
using LabProject.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject_Test
{
    public class OrderDetailServiceTests
    {

        OrderDetail testOrderDetail1 = new OrderDetail {OrderId = 1, Sku  = 10, SkuName = "Ariel", Amount = 50, UnitPrice = 10 };
        OrderDetail testOrderDetail2 = new OrderDetail { OrderId = 2, Sku = 20, SkuName = "Micolor", Amount = 150, UnitPrice = 2 };

        [Fact]
        public async Task GetAll_OrderDetails_Success()
        {
            // Arrange
            var orderDetailRepoMock = new Mock<IOrderDetailRepository>();

            testOrderDetail1.Id = new Guid();
            testOrderDetail2.Id = new Guid();

            orderDetailRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<OrderDetail>
            {
                testOrderDetail1, testOrderDetail2
            });
            var orderDetailService = new OrderDetailService(orderDetailRepoMock.Object);

            // Act            
            var result = await orderDetailService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

        }



        [Fact]
        public async Task GetById_OrderDetails_Success()
        {
            // Arrange
            Guid guid1 = new Guid();
            testOrderDetail1.Id = guid1;

            var orderDetailRepoMock = new Mock<IOrderDetailRepository>();

            orderDetailRepoMock.Setup(repo => repo.GetByIdAsync(guid1)).ReturnsAsync(testOrderDetail1);
            var orderDetailService = new OrderDetailService(orderDetailRepoMock.Object);

            // Act            
            var result = await orderDetailService.GetByIdAsync(guid1);


            // Assert
            Assert.NotNull(result);
            Assert.Equal(guid1, result.Id);

        }



    }
}
