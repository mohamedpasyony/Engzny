using Engzny.BL.Interface;
using Engzny.DAL.Database;
using Engzny.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.BL.Repository
{
    public class RolesRep : IRolesRep
    {
        private readonly RoleManager<IdentityRole> _roleManager;


        public RolesRep(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public Task<IdentityResult> CreateRole(RolesVM role)
        {
            var Role = new IdentityRole
            {
                Name = role.Name
            };
            var state = _roleManager.CreateAsync(Role);
            return state;
        }

        public async Task<IQueryable<RolesVM>> GetRoles()
        {
            var roles =  await _roleManager.Roles.Select(a => new RolesVM
            {
                Id = a.Id,
                Name=a.Name
               
            }).ToListAsync();

            return roles.AsQueryable();
        }

        public  Task<IdentityRole> GetRoleById(string id)
        {
            var role =  _roleManager.FindByIdAsync(id);
          
           
            return role;
            
            
        }

        public  Task<IdentityResult> UpdateRole(IdentityRole role)
        {
            var state= _roleManager.UpdateAsync(role);
            return state;

        }

        public  Task<IdentityResult> DeleteRole(string id , IdentityRole role)
        {
            var state=  _roleManager.DeleteAsync(role);
            return state;
        }

        
        
    }
}
