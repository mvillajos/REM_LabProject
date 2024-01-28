using LabProject.Models.Sales;

namespace LabProject.Services.Interfaces
{
    public interface IDashboardService
    {
        public Task<CustomerOrdersCost> GetCustomerMorePayments();
        public Task<List<ItemsSold>> GetTopSoldItems(int numItems);
    }
}
