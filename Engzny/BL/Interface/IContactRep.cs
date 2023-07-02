using Engzny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.BL.Interface
{
   public interface IContactRep
    {
       Task< List<ContactVM>> Get();

        Task Add(ContactVM Contact);
       Task< ContactVM> GetById(int id);
        Task<int> GetRecentRecordId();
        Task Delete(int id);



    }
}
