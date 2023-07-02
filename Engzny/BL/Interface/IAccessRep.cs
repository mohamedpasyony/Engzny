using Engzny.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.BL.Interface
{
  public  interface IAccessRep
    {
        Task<IdentityResult> Add(RegestrationVM Regestration);
        Task<SignInResult> Login(LoginVM Login);
        Task LogOut();
        Task<IdentityUser> FindByEmail(String Email);
        Task<string> GetToken(IdentityUser User);
        Task<IdentityResult> ResetPassword(IdentityUser User, string Token , string Password);
        Task<IdentityResult> AddRole(RegestrationVM Regestration);



    }
}
