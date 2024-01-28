using LabProject.Data;
using LabProject.Models;
using LabProject.Models.Customers;
using LabProject.Models.Sales;
using LabProject.Services;
using LabProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabProject.Controllers
{
    public class OrdersController : Controller
    {

        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        // GET: Orders
        public async Task<IActionResult> Index()
        {

            var listaOrders = await _orderService.GetAllAsync();
            return View(listaOrders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "id not specified" });
            }

            var order = await _orderService.GetByIdAsync((int)id);
            if (order.Id == -1)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Order not found" });
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.CreateAsync(order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "id not specified" });
            }

            var order = await _orderService.GetByIdAsync((int)id);
            if (order.Id == -1)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Order not found" });
            }

            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,OrderDate")] Order order)
        {
            if (id != order.Id)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "id not specified" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _orderService.UpdateAsync(order);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var orderExists = await _orderService.OrderExists(order.Id);
                    if (!orderExists)
                    {
                        return View("Error", new ErrorViewModel { ErrorMessage = "Order not found" });
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "id not specified" });
            }

            var order = await _orderService.GetByIdAsync((int)id);
            if (order.Id == -1)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Order not found" });
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
