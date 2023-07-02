using Engzny.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.BL.Interface
{
   public interface IUserRep
    {
        IQueryable<UsersVM> GetUsers();
        Task<IdentityUser> GetUserById(string id);
        Task<IdentityResult> UpdateUser(IdentityUser User);
        Task<IdentityResult> DeleteUser(IdentityUser User);
        Task<IList<string>> GetRoles(string id);
        Task<IdentityResult> DeleteRole(string Role, string id);
        Task<IdentityResult> AddRole(AddRoleForUserVM Model);

    }
}
