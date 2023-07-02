using Engzny.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.BL.Interface
{
   public interface IRolesRep
    {
        Task<IdentityResult> CreateRole(RolesVM role);
        Task<IQueryable<RolesVM>> GetRoles();
        Task<IdentityRole>GetRoleById(string id);
        Task<IdentityResult> UpdateRole(IdentityRole role);
        Task<IdentityResult> DeleteRole(string id  , IdentityRole role);




    }
}
