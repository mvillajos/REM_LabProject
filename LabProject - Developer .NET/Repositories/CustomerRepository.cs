using LabProject.Data;
using LabProject.Models.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabProject.Repositories.Interfaces;

namespace LabProject.Repositories
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly CustomersContext _db;

        public CustomerRepository(CustomersContext con)
        {
            _db = con;
        }

        public async Task<List<Customer>> GetAllAsync()
        { 
            return await _db.Customer.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _db.Customer.Where(a => a.Id == id)
                .FirstOrDefaultAsync()?? new Customer { Id=-1, Name="NOT FOUND"};
        }

        public async Task CreateAsync(Customer customer)
        {
          
            await _db.Customer.AddAsync(customer);
            await _db.SaveChangesAsync();                     
        }

        public async Task UpdateAsync(Customer customer)
        {
            _db.Customer.Update(customer);
            await _db.SaveChangesAsync();           
        }


        public async Task DeleteAsync(Customer customer)
        { 
            _db.Customer.Remove(customer);
            await _db.SaveChangesAsync();
        }


    }
}
