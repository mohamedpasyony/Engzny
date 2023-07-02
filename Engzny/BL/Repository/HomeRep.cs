using Engzny.BL.Interface;
using Engzny.DAL.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.BL.Repository
{
    public class HomeRep : IHomeRep
    {
        private readonly Context _Context;
        public HomeRep(Context context)
        {
            _Context = context;
            

        }

        public async Task<int> GetContactsCount()
        {
            var Count = await _Context.Contacts.CountAsync();
            return Count;
        }

        public async Task<int> GetCraftmensCount()
        {
            var Count = await  _Context.Craftmens.CountAsync();
            return Count;
        }

        public async Task<int> GetOrdersCount()
        {
            var Count = await  _Context.Orders.CountAsync();
            return Count;
        }

        public async Task<int> GetServicesCount()
        {
            var Count = await  _Context.Services.CountAsync();
            return Count;
        }

        public async Task<int> GetUsersCount()
        {
           var Count= await  _Context.Users.CountAsync();
            return Count;
        }
    }
}
