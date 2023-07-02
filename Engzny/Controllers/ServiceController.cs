using Engzny.BL.Interface;
using Engzny.BL.Repository;
using Engzny.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Controllers
{
    [Authorize(Roles = "Admin,Manger")]

    public class ServiceController : Controller
    {

        private readonly IServiceRep _Service;

        public ServiceController(IServiceRep Service)
        {
            _Service = Service;


        }
        public async Task< IActionResult> Index()
        {
            var Services = await _Service.Get();
            return View(Services);
        }
        public IActionResult Create()
        {
           
            return View();

        }
        [HttpPost]
        public async Task< IActionResult> Create(ServiceVM Service)
        {
            var files = Request.Form.Files;
            if (!files.Any())
            {
                ModelState.AddModelError("Image", "Image is required!!");
                return View(Service);
            }
            using var dataStream = new MemoryStream();
            var Image = files.FirstOrDefault();
            Image.CopyTo(dataStream);
            Service.Image = dataStream.ToArray();
            await _Service.Add(Service);
            return RedirectToAction("Index");

        }

        public async Task< IActionResult> Update(int id)
        {
            var Service = await _Service.GetById(id);
            if (Service == null)
                return NotFound();

            return View(Service);

        }
        [HttpPost]
        public async Task< IActionResult> Update(ServiceVM Service)
        {
            var files = Request.Form.Files;
            if (files.Any())
            {
                using var dataStream = new MemoryStream();
                var Image = files.FirstOrDefault();
                Image.CopyTo(dataStream);
                Service.Image = dataStream.ToArray();
            }
           
           await  _Service.Edit(Service);
            return RedirectToAction("Index");

        }
        public async Task< IActionResult> Delete(byte id)
        {
            await _Service.Delete(id);
            return Ok();

        }
    }
}
