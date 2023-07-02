using Engzny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.BL.Interface
{
  public  interface ICraftmanRep
    {
        Task< List <CraftmanVM>> Get();
        Task Add(CraftmanVM Craftman);
       Task< CraftmanVM> IncludeLists();
      Task< CraftmanVM>GetById(int id);
        Task Edit(CraftmanVM Craftman);
        Task Delete(int id);



    }
}
