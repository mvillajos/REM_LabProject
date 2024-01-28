using LabProject.Repositories.Interfaces;
using LabProject.Models.Customers;
using LabProject.Services.Interfaces;
using LabProject.Repositories;
using LabProject.Models.Sales;

namespace LabProject.Services
{
    public class DashBoardService: IDashboardService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderDetailRepository _orderDetailsRepo;
        public DashBoardService(ICustomerRepository customerRepo, IOrderRepository orderRepo, IOrderDetailRepository orderDetailsRepo)
        {
            _customerRepo = customerRepo;
            _orderRepo = orderRepo;
            _orderDetailsRepo = orderDetailsRepo;
        }

        public async Task<CustomerOrdersCost> GetCustomerMorePayments() {
            CustomerOrdersCost result=new CustomerOrdersCost();

            result.Customer.Name = "";
            result.Customer.Surname = "";
            result.Customer.Lastname = "";


            List<CustomerOrdersCost> customersOrdersCost = await _orderRepo.GetOrdersCostByCustomer();

            if (customersOrdersCost.Count == 0)
            {
                result.Customer.Id = -1;
            }
            else
            {
                //El primer Customer de la lista, por estar ordenada en sentido descendente por OrdersCost
                //será el que haya gastado más en productos
                CustomerOrdersCost customerMoreCost = customersOrdersCost.First();

                result.Customer.Id = customerMoreCost.Customer.Id;
                result.OrdersCost = customerMoreCost.OrdersCost;

                //Buscar a partir de su Id el cliente para obtener apellidos y nombre
                Customer customerFound = await _customerRepo.GetByIdAsync(customerMoreCost.Customer.Id);
               

                //Si encontramos customer a partir de su Id rellenar Name, Surname y LastName
                if (customerFound.Id != -1)
                {
                    result.Customer.Name = customerFound.Name;
                    result.Customer.Surname = customerFound.Surname;
                    result.Customer.Lastname = customerFound.Lastname;
                }

            }

            return result;
        }

        public async Task<List<ItemsSold>> GetTopSoldItems(int numItems)
        {
            List<ItemsSold> result;

            result = await _orderDetailsRepo.GetItemsSoldBySku(numItems);

            return result;
        }

    }
}
