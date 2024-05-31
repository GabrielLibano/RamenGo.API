using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamenGo.Domain.ResponseModel
{
  public class ProteinsModel
  {
    public ProteinsModel() { }
    public string Id { get; set; }
    public string ImageInactive { get; set; }
    public string ImageActive { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }

  }
}
