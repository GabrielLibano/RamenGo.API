using RamenGo.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamenGo.Service.Interfaces
{
  public interface IIngredientsService
  {
    List<BrothModel> GetBroths();
    List<ProteinsModel> GetProteins();
  }
}
