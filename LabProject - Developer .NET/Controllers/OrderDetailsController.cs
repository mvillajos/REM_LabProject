using LabProject.Data;
using LabProject.Models;
using LabProject.Models.Sales;
using LabProject.Services;
using LabProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LabProject.Controllers
{
    public class OrderDetailsController : Controller
    {

        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderService _orderService;

        public OrderDetailsController(IOrderDetailService orderDetailService, IOrderService orderService)
        {
            _orderDetailService = orderDetailService;
            _orderService = orderService;
        }


        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var listaOrderDetails = await _orderDetailService.GetAllAsync();
            return View(listaOrderDetails);
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {

            if (id == null)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "id not specified" });
            }

            var orderDetail = await _orderDetailService.GetByIdAsync((Guid)id);
            if (orderDetail.Id == Guid.Empty)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "OrderDetail not found" });
            }

            return View(orderDetail);
          
        }

        // GET: OrderDetails/Create
        public async Task<IActionResult> Create()
        {
            var orders = await _orderService.GetAllAsync();

            ViewData["OrderId"] = new SelectList(orders, "Id", "Id");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,Sku,SkuName,Amount,UnitPrice")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                orderDetail.Id = Guid.NewGuid();
                await _orderDetailService.CreateAsync(orderDetail);
                return RedirectToAction(nameof(Index));
            }

            var orders = await _orderService.GetAllAsync();
            ViewData["OrderId"] = new SelectList(orders, "Id", "Id", orderDetail.OrderId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "id not specified" });
            }

            var orderDetail = await _orderDetailService.GetByIdAsync((Guid)id);
            if (orderDetail.Id==Guid.Empty)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "OrderDetail not found" });
            }
            var orders = await _orderService.GetAllAsync();
            ViewData["OrderId"] = new SelectList(orders, "Id", "Id", orderDetail.OrderId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,OrderId,Sku,SkuName,Amount,UnitPrice")] OrderDetail orderDetail)
        {
            if (id != orderDetail.Id)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "id not specified" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _orderDetailService.UpdateAsync(orderDetail);                  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _orderDetailService.OrderDetailExists(orderDetail.Id))
                    {
                        return View("Error", new ErrorViewModel { ErrorMessage = "OrderDetail not found" });
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var orders = await _orderService.GetAllAsync();
            ViewData["OrderId"] = new SelectList(orders, "Id", "Id", orderDetail.OrderId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            var orderDetailAll = await _orderDetailService.GetAllAsync();
            if (id == null || orderDetailAll.Count == 0)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "id not specified or Entity set 'SalesContext.OrderDetail' is empty. " });
            }

            var orderDetail = await _orderDetailService.GetByIdAsync((Guid)id);
            if (orderDetail.Id == Guid.Empty)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "OrderDetail not found" });
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderDetailAll = await _orderDetailService.GetAllAsync();

            if (orderDetailAll.Count==0)
            { 
                return View("Error", new ErrorViewModel { ErrorMessage = "Entity set 'SalesContext.OrderDetail'  is empty." });
            }
            var orderDetail = await _orderDetailService.GetByIdAsync((Guid)id);
            if (orderDetail.Id != Guid.Empty)
            {
                await _orderDetailService.DeleteAsync(id);
            }
    
            return RedirectToAction(nameof(Index));
        }

      
    }
}
