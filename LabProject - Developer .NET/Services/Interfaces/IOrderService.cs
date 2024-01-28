using LabProject.Models.Sales;

namespace LabProject.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<List<Order>> GetAllAsync();

        public Task<Order> GetByIdAsync(int id);

        public Task CreateAsync(Order order);

        public Task UpdateAsync(Order order);

        public Task DeleteAsync(int id);

        public Task<bool> OrderExists(int id);
    }
}
