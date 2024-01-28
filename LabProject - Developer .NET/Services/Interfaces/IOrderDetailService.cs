using LabProject.Models.Sales;
using LabProject.Repositories.Interfaces;

namespace LabProject.Services.Interfaces
{
    public interface IOrderDetailService
    {

        public Task<List<OrderDetail>> GetAllAsync();

        public Task<OrderDetail> GetByIdAsync(Guid id);


        public Task CreateAsync(OrderDetail orderDetail);

        public Task UpdateAsync(OrderDetail orderDetail);

        public Task DeleteAsync(Guid id);

        public Task<bool> OrderDetailExists(Guid id);

    }
}
