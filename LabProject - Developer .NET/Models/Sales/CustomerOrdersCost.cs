using LabProject.Models.Customers;

namespace LabProject.Models.Sales
{
    public class CustomerOrdersCost
    {
        public Customer Customer { get; set; } = new Customer();
        public double OrdersCost { get; set; }
    }
}
