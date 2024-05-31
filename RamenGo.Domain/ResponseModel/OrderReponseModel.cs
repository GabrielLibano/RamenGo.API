using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamenGo.Domain.ResponseModel
{
  public class OrderResponseModel
  {
    public OrderResponseModel() { }
    public string Id { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }

  }
}
