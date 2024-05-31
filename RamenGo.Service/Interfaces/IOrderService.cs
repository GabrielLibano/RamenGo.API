using RamenGo.Domain.RequestModel;
using RamenGo.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamenGo.Service.Interfaces
{
  public interface IOrderService
  {
    OrderResponseModel MakeOrder(OrderRequestModel orderRequestModel);

  }
}
