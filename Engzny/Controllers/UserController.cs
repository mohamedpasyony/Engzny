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
    public class UserController : Controller
    {
        private readonly IUserRep _userRep;
        private readonly IRolesRep _rolesRep;
        private readonly IToastNotification _ToastNotification;



        public UserController(IUserRep userRep, IRolesRep rolesRep, IToastNotification toastNotification)
        {
            _userRep = userRep;
            _rolesRep = rolesRep;
            _ToastNotification = toastNotification;
        }

        public   IActionResult Index()
        {
            var Users =  _userRep.GetUsers();
            return View(Users);
        }
        public async Task<IActionResult> Roles(string id)
        {
            var user = await _userRep.GetUserById(id);
            if (user != null)
            {
                ViewBag.UserId = id;
                var Roles = await _userRep.GetRoles(id);
                return View(Roles);
            }
            return RedirectToAction("Index");
            
          
        }

        public async Task<IActionResult>DeleteRoleForUser(string id, string rname)
        {
            if(id!=null && rname != null)
            {
                //var Roles = await _userRep.GetRoles(id);
                var state = await _userRep.DeleteRole(rname, id);
                if (state.Succeeded)
                {
                    return Ok();

                }
            }
          
            return RedirectToAction("Index");


        }
        public async Task< IActionResult> AddRoleForUser(string id)
        {
            var user = await _userRep.GetUserById(id);
            if (user != null)
            {
                var data = new AddRoleForUserVM
                {
                    Id = id,
                    Roles = await _rolesRep.GetRoles()
                };
                return View(data);
            }
            return RedirectToAction("Index");
          
           
           
        }
        [HttpPost]
        public async Task< IActionResult> AddRoleForUser(AddRoleForUserVM Model)
        {

            if (ModelState.IsValid)
            {
                var state = await _userRep.AddRole(Model);
                if (state.Succeeded)
                {
                    _ToastNotification.AddSuccessToastMessage("Role Added to User  successful !");
                    return RedirectToAction("Index");
                }
                else
                {

                    Model.Roles = await _rolesRep.GetRoles();

                    foreach (var Erorr in state.Errors)
                    {
                        ModelState.AddModelError("", Erorr.Description);
                    }
                } 


            }
            return View(Model);
        }

        public async Task<IActionResult> Update(string id)
        {
            var UserDB = await _userRep.GetUserById(id);
            if (UserDB == null)
                return NotFound();
            var User = new UsersVM
            {
                UserId=UserDB.Id,
                UserName=UserDB.UserName,
                Email=UserDB.Email

            };
            return View(User);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UsersVM User)
        {
            if (ModelState.IsValid)
            {
                var UserDB = await _userRep.GetUserById(User.UserId);
                UserDB.UserName = User.UserName;
                UserDB.Email = User.Email;
                var state = await _userRep.UpdateUser(UserDB);
                if (state.Succeeded)
                {
                    _ToastNotification.AddSuccessToastMessage("User Update Successfully !");
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
            return View(User);

        }

        public async Task<IActionResult> Delete(string Id)
        {
            var User = await _userRep.GetUserById(Id);
            var state = await _userRep.DeleteUser( User);
            if (state.Succeeded)
            {

                return Ok();
            }
            return RedirectToAction("Index");

        }





    }
}
