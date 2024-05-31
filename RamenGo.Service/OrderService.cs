using Newtonsoft.Json;
using RamenGo.Domain.RequestModel;
using RamenGo.Domain.ResponseModel;
using RamenGo.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RamenGo.ACL.Interfaces;
namespace RamenGo.Service
{
  public class OrderService : IOrderService
  {
    private readonly IRedventureAPI _redventureAPI;
    

    public OrderService(IRedventureAPI redventureAPI)
    {
      _redventureAPI = redventureAPI;
    }

    public OrderResponseModel MakeOrder(OrderRequestModel orderRequestModel)
    {
      if (orderRequestModel == null)
        return null;

      var dataResultApi = _redventureAPI.MakeOrder(orderRequestModel.BrothId.ToString(), orderRequestModel.ProteinId.ToString());
      string orderId = (string)dataResultApi["orderId"];
      OrderResponseModel orderResponseModel = new OrderResponseModel()
      {
        Image = "https://tech.redventures.com.br/icons/ramen/ramenChasu.png",
        Description = "Salt and Chasu Ramen",
        Id = orderId,
      };

      return orderResponseModel;
    }

  }
}
