using LabProject.Data;
using LabProject.Models.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabProject.Services.Interfaces;
using LabProject.Models;

namespace LabProject.Controllers
{
    public class CustomersController : Controller
    {
       
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {           
            _customerService = customerService;
        }

      
        public async Task<IActionResult> Index()
        {
            var listaCustomers = await _customerService.GetAllAsync();
            return View(listaCustomers);
        }


        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {              
                return View("Error", new ErrorViewModel { ErrorMessage = "id not specified" });
            }
           
            var customer = await _customerService.GetByIdAsync((int)id);
            if (customer.Id == -1)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Customer not found" });
            }
            
            return View(customer);
        }


        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Lastname,DateOfBirthday,Email,CountryOfResidence")] Customer customer)
        {
            if (ModelState.IsValid)
            {                
                await _customerService.CreateAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

      
        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           
            if (id == null)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "id not specified" });
            }

            var customer = await _customerService.GetByIdAsync((int)id);
            if (customer.Id == -1)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Customer not found" });
            }

            return View(customer);

        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Lastname,DateOfBirthday,Email,CountryOfResidence")] Customer customer)
        {
            if (id != customer.Id)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "id not specified" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _customerService.UpdateAsync(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var customerExists = await _customerService.CustomerExists(customer.Id);
                    if (!customerExists )
                    {
                        return View("Error", new ErrorViewModel { ErrorMessage = "Customer not found" });
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
           
            if (id == null)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "id not specified" });
            }

            var customer = await _customerService.GetByIdAsync((int)id);
            if (customer.Id == -1)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Customer not found" });
            }
         
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          
            await _customerService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
     
    }
}
