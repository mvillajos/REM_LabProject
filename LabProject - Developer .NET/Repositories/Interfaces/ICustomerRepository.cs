using LabProject.Models.Customers;

namespace LabProject.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<List<Customer>> GetAllAsync();
        public Task<Customer> GetByIdAsync(int id);
        public Task CreateAsync(Customer customer);
        public Task DeleteAsync(Customer customer);
        public Task UpdateAsync(Customer customer);
    }
}
