using LabProject.Models.Sales;

namespace LabProject.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Task<List<CustomerOrdersCost>> GetOrdersCostByCustomer();

        public Task<List<Order>> GetAllAsync();

        public Task<Order> GetByIdAsync(int id);

        public Task CreateAsync(Order order);

        public Task UpdateAsync(Order order);

        public Task DeleteAsync(Order order);
    }
}
