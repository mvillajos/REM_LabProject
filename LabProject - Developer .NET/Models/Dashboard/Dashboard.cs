using LabProject.Models.Customers;
using LabProject.Models.Sales;

namespace LabProject.Models.Dashboard
{
    public class Dashboard
    {
        public CustomerOrdersCost customerMorePayments { get; set; } = new CustomerOrdersCost();
        public List<Customer> customersList { get; set; } = new List<Customer>();
        
    }
}
