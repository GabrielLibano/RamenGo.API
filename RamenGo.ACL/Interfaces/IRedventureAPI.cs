using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamenGo.ACL.Interfaces
{
  public interface IRedventureAPI
  {
    dynamic MakeOrder(string brothId, string proteinId);
  
    }
}
