using LabProject.Models.Customers;
using LabProject.Repositories;
using LabProject.Repositories.Interfaces;
using LabProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabProject.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerService(ICustomerRepository customerRepo)
        { 
            _customerRepo = customerRepo;
        }

        public async Task<List<Customer>> GetAllAsync()
        { 
            return await _customerRepo.GetAllAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        { 
            return await _customerRepo.GetByIdAsync(id);
        }


        public async Task CreateAsync(Customer customer)
        {
            await _customerRepo.CreateAsync(customer);
        }

        public async Task UpdateAsync(Customer customer)
        {
             await _customerRepo.UpdateAsync(customer);
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _customerRepo.GetByIdAsync(id);
            if (customer.Id!=-1)
                await _customerRepo.DeleteAsync(customer);
        }

        public async Task<bool> CustomerExists(int id)
        {
            var customer = await _customerRepo.GetByIdAsync(id);
            return customer.Id != -1;
        }

    }
}
