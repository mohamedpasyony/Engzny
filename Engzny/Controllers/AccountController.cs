using Engzny.BL.Interface;
using Engzny.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NToastNotify;
using Microsoft.AspNetCore.Authorization;
using Engzny.BL.Helper;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Engzny.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccessRep _accessRep;
        private readonly IToastNotification _ToastNotification;
        private readonly IMailHelper _mailHelper;


        public AccountController(IAccessRep accessRep, IToastNotification toastNotification, IMailHelper mailHelper)
        {
            _accessRep = accessRep;
            _ToastNotification = toastNotification;
            _mailHelper = mailHelper;
        }


        public  IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM Login)
        {
            if (ModelState.IsValid)
            {
              var state= await _accessRep.Login(Login);
                if (state.Succeeded)
                {
                    _ToastNotification.AddSuccessToastMessage($" Welcome {Login.UserName} To Engzny Dashboard");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email or Password");
                }
            }
            return View(Login);
        }

        public IActionResult ForgetPassword()
        {

            return View();
        }
       

        [HttpPost]
        public async Task<IActionResult>ForgetPassword(ForgetPasswordVM ForgetPassword)
        {
            if (ModelState.IsValid)
            {
                var user= await _accessRep.FindByEmail(ForgetPassword.Email);

                if (user!=null)
                {
                    var token= await  _accessRep.GetToken(user);
                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = ForgetPassword.Email, Token = token }, Request.Scheme);
                    var bodyBuilder = new StringBuilder();
                    bodyBuilder.AppendLine("<html>");
                    bodyBuilder.AppendLine("<body>");
                    bodyBuilder.AppendLine("<h1>Password Reset</h1>");
                    bodyBuilder.AppendLine("<p>Dear User,</p>");
                    bodyBuilder.AppendLine("<p>We received a request to reset your password. Click the link below to reset it:</p>");
                    bodyBuilder.AppendLine("<p><a href=\"" + passwordResetLink + "\">Reset Password</a></p>");
                    bodyBuilder.AppendLine("<p>If you did not request a password reset, please ignore this email.</p>");
                    bodyBuilder.AppendLine("<p>Best regards,<br>Engzny Team </p>");
                    bodyBuilder.AppendLine("</body>");
                    bodyBuilder.AppendLine("</html>");
                    await _mailHelper.SendMail(ForgetPassword.Email, "Reset Password link", bodyBuilder.ToString());
                    return RedirectToAction("ConfirmForgetPassword");

                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email");
                }

            }
            return View(ForgetPassword);
        }
        public IActionResult ConfirmForgetPassword()
        {

            return View();
        }



        public IActionResult ResetPassword(string Email , string Token)
        {
            if(Email==null || Token == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> ResetPassword(ResetPasswordVM ResetPassword)
        {
            if (ModelState.IsValid)
            {
                var user =await _accessRep.FindByEmail(ResetPassword.Email);
                if (user != null)
                {
                    var state = await _accessRep.ResetPassword(user, ResetPassword.Token, ResetPassword.Password);
                    if (state.Succeeded)
                    {
                        return RedirectToAction("ConfirmResetPassword");
                    }
                    foreach (var error in state.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(ResetPassword);

                }
              
            }
            return View(ResetPassword);
        }

        public IActionResult ConfirmResetPassword()
        {

            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _accessRep.LogOut();
          return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
