using Engzny.BL.Interface;
using Engzny.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.BL.Repository
{
    public class AccessRep : IAccessRep
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private IdentityUser User { get; set; }


        public AccessRep(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public Task<IdentityResult> Add(RegestrationVM Regestration)
        {
             User = new IdentityUser
            {
                UserName = Regestration.UserName,
                Email = Regestration.Email,

            };
            var state = _userManager.CreateAsync(User, Regestration.Password);
            return state;
        }

        public Task<IdentityResult> AddRole(RegestrationVM Regestration)
        {
           
           var state=  _userManager.AddToRoleAsync(User, Regestration.Role);
            return state;
        }

        public  Task<IdentityUser>  FindByEmail(String Email)
        {
            var  user =  _userManager.FindByEmailAsync(Email);
            return user;
        }

        public  Task<string> GetToken( IdentityUser user)
        {
            var token =  _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        public Task<SignInResult> Login(LoginVM Login)
        {
            var state = _signInManager.PasswordSignInAsync(Login.UserName, Login.Password, Login.RememberMe, false);
            return state;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ResetPassword(IdentityUser User, string Token, string Password)
        {

            var result = await _userManager.ResetPasswordAsync(User, Token, Password);
            return result;
        }

       
    }
}
