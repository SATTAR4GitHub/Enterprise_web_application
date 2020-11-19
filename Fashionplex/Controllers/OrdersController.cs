using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fashionplex.Data;
using Fashionplex.Models;
using Fashionplex.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Fashionplex.Controllers
{
    /// <summary>
    /// This class controlls all the incoming orders made by the customers.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor to initialize DbContext
        /// </summary>
        /// <param name="context"></param>
        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// // GET: Orders
        /// Display all the orders and sort orders when clicking on the table column header
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string sortOrder, int? pageNumber)
        {
            int pageSize = 5;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["OrderStatusSortParm"] = String.IsNullOrEmpty(sortOrder) ? "orderStatus_asc" : "";
            ViewData["OrderNumberSortParm"] = String.IsNullOrEmpty(sortOrder) ? "orderNumber_asc" : "";
            ViewData["CustomerNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "customerName_asc" : "";

            IQueryable<Order> orders = _context.Orders.Include(o => o.Customer).Include(o => o.ShipmentInfo);

            switch (sortOrder)
            {
                case "orderStatus_asc":
                    orders = orders.OrderBy(o => o.OrderStatus);
                    break;
                case "orderNumber_asc":
                    orders = orders.OrderBy(o => o.Id);
                    break;
                case "customerName_asc":
                    orders = orders.OrderBy(o => o.Customer.FirstName);
                    break;
                default:
                    orders = orders.OrderByDescending(o => o.CreateDate);
                    break;
            }

            return View(await Pagination<Order>.CreateAsync(orders.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        /// <summary>
        /// GET: Orders/Details/5
        /// Return order details for each order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.ShipmentInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        /// <summary>
        /// GET: Orders/Edit/5
        /// Method to update an order.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
            ViewData["ShipmentId"] = new SelectList(_context.Shipments, "Id", "Id", order.ShipmentId);

            //Order status drop down from enum value
            OrderStatusDropDown();

            return View(order);
        }

        /// <summary>
        /// POST: Orders/Edit/5
        /// Method to submit the updated order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(long id, [Bind("OrderTotal,OrderItemTotal,ShippingCost,CustomerId,ShipmentId,OrderStatus,Id,CreateDate,ModifiedDate,IsDeleted")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
            ViewData["ShipmentId"] = new SelectList(_context.Shipments, "Id", "Id", order.ShipmentId);
            return View(order);
        }

        /// <summary>
        /// GET: Orders/Delete/5
        /// Method that renders an order when clcik the delete button 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.ShipmentInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        /// <summary>
        /// POST: Orders/Delete/5
        /// Complete the delete process of an order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Check if any order exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool OrderExists(long id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        /// <summary>
        /// Helper method for product size drop down from enum value
        /// </summary>
        private void OrderStatusDropDown()
        {
            var orderStatus = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select",
                    Value = " "
                }
            };

            foreach (OrderStatus item in Enum.GetValues(typeof(OrderStatus)))
            {
                orderStatus.Add(new SelectListItem { Text = Enum.GetName(typeof(OrderStatus), item), Value = item.ToString() });
            }

            ViewBag.OrderStatus = orderStatus;
        }
    }
}
