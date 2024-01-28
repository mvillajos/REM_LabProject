using LabProject.Repositories.Interfaces;
using LabProject.Repositories;
using LabProject.Services;
using LabProject.Services.Interfaces;
using LabProject.Models.Customers;
using Moq;

namespace LabProject_Test
{
    public class CustomerServiceTests
    {
        
        Customer testCustomer1 = new Customer { Id = 1, Name = "John", Surname = "Wick", Lastname = "Smith" };
        Customer testCustomer2 = new Customer { Id = 2, Name = "Pepe", Surname = "Alvarez", Lastname = "Ortega" };

        [Fact]
        public async Task GetAll_Customers_Success()
        {
            // Arrange
            var customerRepoMock = new Mock<ICustomerRepository>();
            
            customerRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Customer>
            {
                testCustomer1, testCustomer2
            });
            var customerService = new CustomerService(customerRepoMock.Object);

            // Act            
            var result = await customerService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

        }



        [Fact]
        public async Task GetById_Customers_Success()
        {
            // Arrange
            var customerRepoMock = new Mock<ICustomerRepository>();

            customerRepoMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(testCustomer1);
            var customerService = new CustomerService(customerRepoMock.Object);

            // Act            
            var result = await customerService.GetByIdAsync(1);
           

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);

        }


    }
}