using LabProject.Models.Sales;

namespace LabProject.Repositories.Interfaces
{
    public interface IOrderDetailRepository
    {
        public Task<List<ItemsSold>> GetItemsSoldBySku(int numItems);

        public Task<List<OrderDetail>> GetAllAsync();

        public Task<OrderDetail> GetByIdAsync(Guid id);

        public Task CreateAsync(OrderDetail orderDetail);

        public Task UpdateAsync(OrderDetail orderDetail);

        public Task DeleteAsync(OrderDetail orderDetail);
    }
}
