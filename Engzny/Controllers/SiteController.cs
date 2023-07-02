using Engzny.BL.Interface;
using Engzny.BL.Repository;
using Engzny.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engzny.BL.hub;

namespace Engzny.Controllers
{
    public class SiteController : Controller
    {
        private readonly IServiceRep _Service;
        private readonly IContactRep _Contact;
        private readonly IOrderRep _Order;
        private readonly IHubContext<NotifiactionHub> _HubContext;




        public SiteController(IServiceRep Service, IContactRep contact, IOrderRep order, IHubContext<NotifiactionHub> hubContext)
        {
            _Service = Service;
            _Contact = contact;
            _Order = order;
            _HubContext = hubContext;
        }
        public async Task<IActionResult>Index()
        {
            var Data = await _Service.Get();

            return View(Data);
        }

        public async Task<IActionResult> Order()
        {
            var Data = await _Order.IncludeLists();

            return View(Data);
        }
        [HttpPost]
        public async Task<IActionResult> Order(OrderVM order)
        {
            await _Order.Add(order);
            var OrderDB = await _Order.GetRecentOrder();

            await _HubContext.Clients.All.SendAsync
                (
                "newOrderNotification",
                order.Name ,
                " New Order From ",
                order.OrderDateTime
                );

            await _HubContext.Clients.All.SendAsync
                (
                "newOrder",
                order.Name,
                order.Phone,
                order.OrderDateTime.ToString("dddd, dd MMMM  hh:mm:ss tt"),
                OrderDB.Service.Name,
                OrderDB.Id
                );
            return RedirectToAction("ConfirmOrder");
        }

        public IActionResult ContactUs()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactUs(ContactVM Contact)
        { 
            await _Contact.Add(Contact);
            var Id =await _Contact.GetRecentRecordId();
            await _HubContext.Clients.All.SendAsync
                (
                "newContactNotification",
                Contact.Name, 
                " New Contact From ",
                Contact.ContactDateTime
                );


            await _HubContext.Clients.All.SendAsync
                (
                "NewContact",
                Contact.Name,
                Contact.Email,
                Contact.Phone,
                Contact.ContactDateTime.ToString("dddd, dd MMMM  hh:mm:ss tt"),
                Id
                );

            return RedirectToAction("ConfirmContact");

        }

        public IActionResult Details(string name)
        {
            if (name == "الكهرباء")
            {
                return RedirectToAction("ElectricityDetails","ServiceDetails");
            }
            if (name == "الحدادة")
            {
                return RedirectToAction("blacksmithingDetails", "ServiceDetails");
            }
            if (name == "النجارة")
            {
                return RedirectToAction("CarpentryDetails", "ServiceDetails");
            }
            if (name == "السباكة")
            {
                return RedirectToAction("PlumbingDetails", "ServiceDetails");
            }
            if (name == "النقل والتوصيل")
            {
                return RedirectToAction("TransportDetails", "ServiceDetails");
            }
            if (name == "الاجهزة المنزلية")
            {
                return RedirectToAction("HomeappliancesDetails", "ServiceDetails");
            }
            return RedirectToAction("ComingSoon", "ServiceDetails");
        }


       
        public IActionResult ConfirmContact()
        {

            return View();
        }

        public  IActionResult ConfirmOrder()
        {
            return View();
        }


        public IActionResult AboutUs()
        {

            return View();
        }

        public IActionResult HowToUse()
        {

            return View();
        }
    }
}
