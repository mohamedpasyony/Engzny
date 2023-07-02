using Engzny.BL.Interface;
using Engzny.BL.Repository;
using Engzny.DAL.Database;
using Engzny.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IHomeRep _Home;

        public HomeController(IHomeRep Home)
        {
            _Home = Home;


        }
        public async Task<IActionResult> Index()
        {
            ViewBag.CountServices = await _Home.GetServicesCount();
            ViewBag.CountCraftmens = await _Home.GetCraftmensCount();
            ViewBag.CountContacts = await _Home.GetContactsCount();
            ViewBag.CountOrders = await _Home.GetOrdersCount();
            ViewBag.CountUsers = await _Home.GetUsersCount();

            return View();
        }

          
        }
    }

