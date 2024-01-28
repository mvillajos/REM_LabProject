using LabProject.Data;
using LabProject.Models.Sales;
using LabProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabProject.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {

        private readonly SalesContext _db;

        public OrderDetailRepository(SalesContext con)
        {
            _db = con;
        }

        public async Task<List<ItemsSold>> GetItemsSoldBySku(int numItems)
        {
            List<ItemsSold> result;

            result = await (from od in _db.OrderDetail
                            group od by new { od.Sku, od.SkuName } into g
                            select new ItemsSold
                            {
                                Sku = g.Key.Sku,
                                SkuName = g.Key.SkuName,
                                TotalAmount = g.Sum(x => x.Amount)
                            }).OrderByDescending(x => x.TotalAmount).Take<ItemsSold>(numItems).ToListAsync();

            return result;
        }


        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await _db.OrderDetail.Include(o => o.Order).ToListAsync();
        }

        public async Task<OrderDetail> GetByIdAsync(Guid id)
        {
            return await _db.OrderDetail.Include(o => o.Order).Where(a => a.Id == id)
                .FirstOrDefaultAsync() ?? new OrderDetail { Id = Guid.Empty};
        }

        public async Task CreateAsync(OrderDetail orderDetail)
        {
            await _db.OrderDetail.AddAsync(orderDetail);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderDetail orderDetail)
        {
            _db.OrderDetail.Update(orderDetail);
            await _db.SaveChangesAsync();
        }


        public async Task DeleteAsync(OrderDetail orderDetail)
        {
            _db.OrderDetail.Remove(orderDetail);
            await _db.SaveChangesAsync();
        }


    }
}
