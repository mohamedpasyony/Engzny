using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.BL.Interface
{
   public interface IHomeRep
    {
        Task<int> GetServicesCount();
        Task<int> GetCraftmensCount();
        Task<int> GetContactsCount();
        Task<int> GetOrdersCount();
        Task<int> GetUsersCount();

    }
}
