using Engzny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.BL.Interface
{
   public interface IOrderRep
    {
        Task< IQueryable<OrderVM>> Get();
        Task<IQueryable<OrderVM>> GetOrdersByStatus(int id);
        Task< OrderVM> GetById(int id);
        Task<OrderVM> GetRecentOrder();
        Task Edit(OrderVM Order);
        Task Delete(int id);
        Task<OrderVM> IncludeLists();
        Task Add(OrderVM order);

    }
}
