using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamenGo.Domain.RequestModel
{
  public class OrderRequestModel
  {
    public OrderRequestModel() { }

    public int BrothId { get; set; }

    public int ProteinId { get; set; }

  }
}
