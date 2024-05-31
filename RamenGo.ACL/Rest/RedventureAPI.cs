using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RamenGo.ACL.Interfaces;
using RamenGo.ACL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RamenGo.ACL.Rest
{
  public class RedventureAPI : IRedventureAPI
  {

    private string BaseUrl = "https://api.tech.redventures.com.br"; //Desculpa não consegui colocar algum meio de proteção como .env ou como eu fiz na controller por meio do appSettings :(
    private string key = "ZtVdh8XQ2U8pWI2gmZ7f796Vh8GllXoN7mr0djNf";
    private static HttpClient httpClient = new HttpClient();
    public dynamic MakeOrder(string brothId, string proteinId)
    {
      try
      {
        OrderRequestModel orderRequestModel = new OrderRequestModel() { ProteinId = proteinId, BrothId = brothId };
        var stringContent = new StringContent(orderRequestModel.ToString());

        httpClient.DefaultRequestHeaders.Add("x-api-key", key);
        HttpResponseMessage responseApi = httpClient.PostAsync($"{BaseUrl}/orders/generate-id", stringContent).Result;

        if (responseApi.StatusCode != System.Net.HttpStatusCode.OK)
          return null;

        responseApi.EnsureSuccessStatusCode();
        string conteudo = responseApi.Content.ReadAsStringAsync().Result;
        JObject orderResponseModel = JObject.Parse(conteudo);
        
        return orderResponseModel;
      }
      catch(Exception ex)
      {
        return null;
      }
    }

  }
}
