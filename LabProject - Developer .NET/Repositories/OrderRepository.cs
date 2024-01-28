using LabProject.Data;
using LabProject.Models.Customers;
using Microsoft.EntityFrameworkCore;
using LabProject.Repositories.Interfaces;
using LabProject.Models.Sales;

namespace LabProject.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly SalesContext _db;

        public OrderRepository(SalesContext con)
        {
            _db = con;
        }

        public async Task<List<CustomerOrdersCost>> GetOrdersCostByCustomer() {

            List<CustomerOrdersCost> result ;

            result = await (from o in _db.Order
                         join od in _db.OrderDetail on o.Id equals od.OrderId
                         group od by o.CustomerId into g
                         select new CustomerOrdersCost
                         {                            
                            Customer = new Customer { Id = g.Key }  ,
                             OrdersCost = g.Sum(x => x.Amount * x.UnitPrice)
                         }).OrderByDescending(x=> x.OrdersCost).ToListAsync();

            return result;
        }


        public async Task<List<Order>> GetAllAsync()
        {
            return await _db.Order.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _db.Order.Where(a => a.Id == id)
                .FirstOrDefaultAsync() ?? new Order { Id = -1 };
        }

        public async Task CreateAsync(Order order)
        {
            await _db.Order.AddAsync(order);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _db.Order.Update(order);
            await _db.SaveChangesAsync();
        }


        public async Task DeleteAsync(Order order)
        {
            _db.Order.Remove(order);
            await _db.SaveChangesAsync();
        }


    }
}
