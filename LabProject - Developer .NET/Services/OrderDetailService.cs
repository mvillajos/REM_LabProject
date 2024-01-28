using LabProject.Models.Sales;
using LabProject.Repositories.Interfaces;
using LabProject.Services.Interfaces;

namespace LabProject.Services
{
    public class OrderDetailService: IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepo;

        public OrderDetailService(IOrderDetailRepository orderDetailRepo)
        {
            _orderDetailRepo = orderDetailRepo;
        }

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await _orderDetailRepo.GetAllAsync();
        }

        public async Task<OrderDetail> GetByIdAsync(Guid id)
        {
            return await _orderDetailRepo.GetByIdAsync(id);
        }


        public async Task CreateAsync(OrderDetail orderDetail)
        {
            await _orderDetailRepo.CreateAsync(orderDetail);
        }

        public async Task UpdateAsync(OrderDetail orderDetail)
        {
            await _orderDetailRepo.UpdateAsync(orderDetail);
        }

        public async Task DeleteAsync(Guid id)
        {
            var orderDetail = await _orderDetailRepo.GetByIdAsync(id);
            if (orderDetail.Id != Guid.Empty)
                await _orderDetailRepo.DeleteAsync(orderDetail);
        }

        public async Task<bool> OrderDetailExists(Guid id)
        {
            var orderDetail = await _orderDetailRepo.GetByIdAsync(id);
            return orderDetail.Id != Guid.Empty;
        }

    }
}
