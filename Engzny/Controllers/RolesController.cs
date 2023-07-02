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
    public class RolesController : Controller
    {
        private readonly IRolesRep _rolesRep;
        private readonly IToastNotification _ToastNotification;


        public RolesController(IRolesRep rolesRep, IToastNotification toastNotification)
        {
            _rolesRep = rolesRep;
            _ToastNotification = toastNotification;
        }


        public async Task<IActionResult> Index()
        {
            var Roles = await _rolesRep.GetRoles();
            return View(Roles);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RolesVM role)
        {
            if (ModelState.IsValid)
            {
                var state = await _rolesRep.CreateRole(role);
                if (state.Succeeded)
                {
                    _ToastNotification.AddSuccessToastMessage("Role Created Successfully !");
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in state.Errors)
                    {
                        ModelState.AddModelError("", error.Description);


                    }

                }


            }


            return View(role);
        }

        public async Task<IActionResult> Update(string id)
        {
            var roleDB = await _rolesRep.GetRoleById(id);
            if (roleDB == null)
                return NotFound();
            var role = new RolesVM
            {
                Id = roleDB.Id,
                Name = roleDB.Name

            };
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Update(RolesVM role)
        {
            if (ModelState.IsValid)
            {
                var RoleDB = await _rolesRep.GetRoleById(role.Id);
                RoleDB.Name = role.Name;
                var state = await _rolesRep.UpdateRole(RoleDB);
                if (state.Succeeded)
                {
                    _ToastNotification.AddSuccessToastMessage("Role Update Successfully !");
                   return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in state.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }


                }
            }
            return View(role);

        }

        
        public async Task< IActionResult> Delete(string Id)
        {
            var role = await _rolesRep.GetRoleById(Id);
              var state= await _rolesRep.DeleteRole(Id,role);
            if (state.Succeeded)
            {

                return Ok();
            }
            return RedirectToAction("Index");

        }
    }
}

