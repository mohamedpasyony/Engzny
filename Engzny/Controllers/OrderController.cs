using Engzny.BL.Interface;
using Engzny.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Controllers
{
    [Authorize(Roles = "Admin,Manger,Customer Service")]
    public class OrderController : Controller
    {
        private readonly IOrderRep _Order;

        public OrderController(IOrderRep order)
        {
            _Order = order;
        }

        public async Task< IActionResult> Index()
        {
            var Orders = await _Order.Get();
            return View(Orders);
        }

        public async Task<IActionResult> ExecutingOrders()
        {
            var Orders = await _Order.GetOrdersByStatus(2);
            return View(Orders);
        }

        public async Task <IActionResult> PendingOrders()
        {
            var Orders = await _Order.GetOrdersByStatus(1);
            return View(Orders);
        }
        public async Task< IActionResult> DoneOrders()
        {
            var Orders = await _Order.GetOrdersByStatus(3);
            return View(Orders);
        }
        public async Task< IActionResult> Details(int id)
        {
            var Order = await _Order.GetById(id);
            if (Order == null)
               return NotFound();

            return View(Order);
        }

        public async Task< IActionResult>UpdateStatus(int id)
        {
            if (id != 0)
            {
                
                var Order = await _Order.GetById(id);
                if (Order != null)
                {
                    if (Order.Status.Name == "Done")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(Order);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
               
            }
            else
            {
                return RedirectToAction("Index");
            }
           
        }
        [HttpPost]
        public async Task<IActionResult>UpdateStatus(OrderVM Order)
        {
            await _Order.Edit(Order);
            return RedirectToAction("Index");

        }

        public async Task< IActionResult> AddFeedback(int id)
        {
            if (id != 0)
            {
                var Order = await _Order.GetById(id);
                if (Order == null)
                    return NotFound();
                if (Order.FeedBack == null)
                {
                    return View(Order);

                }
                else
                {
                    return RedirectToAction("Index");

                }
            }
            else
            {
                return RedirectToAction("Index");

            }

        }
        [HttpPost]
        public async Task< IActionResult> AddFeedback(OrderVM Order)
        {
            await _Order.Edit(Order);
            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Delete(int id)
        {
            await _Order.Delete(id);
            return Ok();
        }

    }
}
