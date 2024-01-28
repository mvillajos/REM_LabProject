using LabProject.Models.Customers;
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
    public class DashboardServiceTests
    {
        Customer testCustomer1 = new Customer { Id = 1, Name = "John", Surname = "Wick", Lastname = "Smith" };

        [Fact]
        public async Task GetCustomerMorePayments_Success()
        {
            // Arrange
            var customerRepoMock = new Mock<ICustomerRepository>();
            var orderRepoMock = new Mock<IOrderRepository>(); 
            var orderDetailsRepoMock = new Mock<IOrderDetailRepository>();
           
            customerRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(testCustomer1);


            orderRepoMock.Setup(repo => repo.GetOrdersCostByCustomer()).ReturnsAsync(new List<CustomerOrdersCost>
              {
                new CustomerOrdersCost
                {
                    Customer = testCustomer1,
                    OrdersCost = 100
                },
              });

         
            var dashboardService = new DashBoardService(customerRepoMock.Object, orderRepoMock.Object, orderDetailsRepoMock.Object);

            // Act
            var result = await dashboardService.GetCustomerMorePayments();

            // Assert
            Assert.Equal(1, result.Customer.Id);
            Assert.Equal("John", result.Customer.Name);
            Assert.Equal("Wick", result.Customer.Surname);
            Assert.Equal("Smith", result.Customer.Lastname);
            Assert.Equal(100, result.OrdersCost);

        }



        [Fact]
        public async Task GetTopItemsSold_Success()
        {
            // Arrange
            var customerRepoMock = new Mock<ICustomerRepository>();
            var orderRepoMock = new Mock<IOrderRepository>();
            var orderDetailsRepoMock = new Mock<IOrderDetailRepository>();

            customerRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(testCustomer1);

            orderDetailsRepoMock.Setup(repo => repo.GetItemsSoldBySku(It.IsAny<int>())).ReturnsAsync(new List<ItemsSold>
                {
                    new ItemsSold { Sku=100, SkuName="Ariel", TotalAmount=200 },
                    new ItemsSold { Sku=20, SkuName="Colgate", TotalAmount=100 },
                    new ItemsSold { Sku=300, SkuName="Milka", TotalAmount=500 },
                    new ItemsSold { Sku=50, SkuName="Manzanas", TotalAmount=30 },
                    new ItemsSold { Sku=80, SkuName="Naranjas", TotalAmount=70 },
                });


            var dashboardService = new DashBoardService(customerRepoMock.Object, orderRepoMock.Object, orderDetailsRepoMock.Object);

            // Act
            var result = await dashboardService.GetTopSoldItems(5);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5,result.Count);           
        }



    }
}
