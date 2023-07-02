using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.BL.Helper
{
  public interface IMailHelper
    {
        Task SendMail(string Reciver, string Title, string body);
    }
}
