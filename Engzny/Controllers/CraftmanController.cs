using Engzny.BL.Interface;
using Engzny.BL.Repository;
using Engzny.DAL.Database;
using Engzny.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Controllers
{
    [Authorize (Roles ="Admin,Manger")]
    public class CraftmanController : Controller
    {
        private readonly ICraftmanRep _Craftman;
        public CraftmanController( ICraftmanRep Craftman)
        {
            _Craftman = Craftman;
            

        }
        public async Task<IActionResult> Index()
        {
            var Data = await _Craftman.Get();
            return View(Data);
        }

        public async Task<IActionResult> Create()
        {

            var Craftman = await _Craftman.IncludeLists();
            return View(Craftman);
           
        }
        [HttpPost]
        public async Task< IActionResult> Create(CraftmanVM Craftman)
        {
          
               await  _Craftman.Add(Craftman);
                return RedirectToAction("Index");
           
           

        }


        public async Task< IActionResult> Update(int id)
        {
            var Craftman = await _Craftman.GetById(id);
            if (Craftman == null)
                return NotFound();

            return View(Craftman);

        }
        [HttpPost]
        public async Task< IActionResult> Update(CraftmanVM Craftman)
        {
          await _Craftman.Edit(Craftman);
             return RedirectToAction("Index");

        }

        public async Task< IActionResult> Details(int id)
        {
            var Craftman = await _Craftman.GetById(id);
            if (Craftman == null)
                return NotFound();
            return View(Craftman);

        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await _Craftman.Delete(id);
            return Ok();

        }
    }
}
