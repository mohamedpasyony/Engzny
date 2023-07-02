using Engzny.BL.Interface;
using Engzny.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.BL.Repository
{
    public class UserRep : IUserRep
    {
        private readonly UserManager<IdentityUser> _userManager;


        public UserRep(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }


        public  IQueryable<UsersVM> GetUsers()
        {
            var Users = _userManager.Users.Select(a=> new UsersVM { 
            UserId=a.Id,
            UserName=a.UserName,
            Email=a.Email
          
            
            });
            return Users;
        }

        public Task<IdentityUser> GetUserById(string id)
        {
            var User = _userManager.FindByIdAsync(id);


            return User;


        }

        public Task<IdentityResult> UpdateUser(IdentityUser User)
        {
            var state = _userManager.UpdateAsync(User);
            return state;

        }

        public Task<IdentityResult> DeleteUser( IdentityUser User)
        {
            var state = _userManager.DeleteAsync(User);
            return state;
        }


        public async Task<IList<string>> GetRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            
            return roles;
        }

        public async Task<IdentityResult>DeleteRole(string Role,string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var state = await _userManager.RemoveFromRoleAsync(user, Role);
            return state;
        }

        public async Task<IdentityResult> AddRole(AddRoleForUserVM Model)
        {
            var User = await _userManager.FindByIdAsync(Model.Id);
          
            var state = await _userManager.AddToRoleAsync(User, Model.Role);
            return state;
        }
    }
}
