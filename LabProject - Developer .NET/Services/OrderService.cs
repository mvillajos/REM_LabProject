using LabProject.Models.Sales;
using LabProject.Repositories.Interfaces;
using LabProject.Services.Interfaces;

namespace LabProject.Services
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepo;

        public OrderService(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _orderRepo.GetAllAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _orderRepo.GetByIdAsync(id);
        }


        public async Task CreateAsync(Order order)
        {
            await _orderRepo.CreateAsync(order);
        }

        public async Task UpdateAsync(Order order)
        {
            await _orderRepo.UpdateAsync(order);
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order.Id != -1)
                await _orderRepo.DeleteAsync(order);
        }

        public async Task<bool> OrderExists(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            return order.Id != -1;
        }


    }
}
