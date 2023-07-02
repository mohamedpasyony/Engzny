using Engzny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.BL.Interface
{
   public interface IServiceRep
    {
       Task<IQueryable<ServiceVM>> Get();
        Task Add(ServiceVM Service);
        Task< ServiceVM> GetById(int id);
        Task Edit(ServiceVM Service);
        Task Delete(byte id);
    }
}
