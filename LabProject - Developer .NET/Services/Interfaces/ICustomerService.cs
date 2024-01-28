using LabProject.Models.Customers;

namespace LabProject.Services.Interfaces
{
    public interface ICustomerService
    {
        public Task<List<Customer>> GetAllAsync();
        public Task<Customer> GetByIdAsync(int id);
        public Task CreateAsync(Customer customer);
        public Task UpdateAsync(Customer customer);

        public Task DeleteAsync(int id);
        public Task<bool> CustomerExists(int id);
    }
}
