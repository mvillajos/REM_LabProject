using LabProject.Models.ChartJS;
using LabProject.Models.Customers;
using LabProject.Models.Dashboard;
using LabProject.Models.Sales;
using LabProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System.Data;
using static LabProject.Controllers.DashboardController;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LabProject.Controllers
{
    public class DashboardController : Controller
    {

        private readonly IDashboardService _dashboardService;
        private readonly ICustomerService _customerService;

        
        public DashboardController(IDashboardService dashboardService, ICustomerService customerService)
        {
            _dashboardService = dashboardService;
            _customerService = customerService;
        }
       

        public async Task<IActionResult> Index()
        {
            Dashboard result = new Dashboard();

            //Obtener Customer que ha gastado más en productos
            CustomerOrdersCost customerOrdersCost = await _dashboardService.GetCustomerMorePayments();
            result.customerMorePayments = customerOrdersCost;

            //Obtener listado de Customers
            List<Customer> allCustomers = await _customerService.GetAllAsync();
            result.customersList = allCustomers;

            return View(result);
        }


        private string[] getColors(int numColors)
        {
            string[] paleta = { "#0074D9", "#FF4136", "#2ECC40", "#FF851B", "#7FDBFF", "#B10DC9", "#FFDC00", "#001f3f", "#39CCCC", "#01FF70", "#85144b", "#F012BE", "#3D9970", "#111111", "#AAAAAA" };
           
            List<string> colors = new List<string>();
 
            for (int i = 0; i < numColors; i++)
            {
                colors.Add(paleta[i % (paleta.Count() - 1)]);
            }

            return colors.ToArray();
        }


        public async Task<JsonResult> topItemsSoldChartData() 
        {
            
            string[] listColors ;

            List<ItemsSold> topItemsSold = await _dashboardService.GetTopSoldItems(5);

            Chart _chart = new();
            _chart.datasets = new List<Datasets>();

            var datchart = from d in topItemsSold orderby d.SkuName select new { d.SkuName, d.TotalAmount };

            _chart.labels = datchart.Select(x=>x.SkuName).ToArray();

            listColors = getColors(datchart.Count());
            
            _chart.datasets.Add(new Datasets()
            {
                data = datchart.Select(x => x.TotalAmount.ToString()).ToArray(),
                backgroundColor = listColors
            }) ;

            return Json(_chart);

        }

    }



}

