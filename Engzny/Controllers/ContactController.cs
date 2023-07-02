using Engzny.BL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Controllers
{
    [Authorize]

    public class ContactController : Controller
    {
        private readonly IContactRep _Contact;

        public ContactController(IContactRep contact)
        {
            _Contact = contact;
        }

        public async Task< IActionResult> Index()
        {
            var Contacts  =  await _Contact.Get();
            return View(Contacts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var Contact = await _Contact.GetById(id);
            if (Contact == null)
                return NotFound();
            return View(Contact);
        }

        public async Task<IActionResult> Delete(int id)
        {
           await  _Contact.Delete(id);
            return Ok();

        }
    }
}
