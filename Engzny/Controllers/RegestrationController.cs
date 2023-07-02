using Engzny.BL.Interface;
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
    [Authorize(Roles ="Admin")]

    public class RegestrationController : Controller
    {
        private readonly IAccessRep _accessRep;
        private readonly IRolesRep _rolesRep;

        private readonly IToastNotification _ToastNotification;

        public RegestrationController(IAccessRep accessRep, IToastNotification toastNotification, IRolesRep rolesRep)
        {
            _accessRep = accessRep;
            _ToastNotification = toastNotification;
            _rolesRep = rolesRep;
        }

        public async Task< IActionResult >Regestration()
        {
            var data = new RegestrationVM
            {
                Roles = await _rolesRep.GetRoles()
            };
            return View(data);
        }
        [HttpPost]

        public async Task<IActionResult> Regestration(RegestrationVM Regestration)
        {
            if (ModelState.IsValid)
            {
                var state = await _accessRep.Add(Regestration);
                if (state.Succeeded)
                {
                   await _accessRep.AddRole(Regestration);
                  _ToastNotification.AddSuccessToastMessage("User Regestred successful !");
                   return RedirectToAction("Index", "User");
                }
                else
                {

                    Regestration.Roles = await _rolesRep.GetRoles();
                    
                    foreach (var Erorr in state.Errors)
                    {
                        ModelState.AddModelError("", Erorr.Description);
                    }
                }
            }

            return View(Regestration);
        }
    }
}
