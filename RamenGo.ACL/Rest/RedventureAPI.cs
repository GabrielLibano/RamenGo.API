using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RamenGo.ACL.Interfaces;
using RamenGo.ACL.Model;
using RamenGO.ACL;
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

    public IConfiguration Configuration { get; }
    public IConfiguration InitConfiguration()
    {
      var config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
          .Build();
      return config;
    }
    private static HttpClient httpClient = new HttpClient();
    public dynamic MakeOrder(string brothId, string proteinId)
    {
      var configuration = InitConfiguration();
      var appSettingsSection = configuration.GetSection("AppSettings");
      var appSettings = appSettingsSection.Get<AppSettings>();
      try
      {
        OrderRequestModel orderRequestModel = new OrderRequestModel() { ProteinId = proteinId, BrothId = brothId };
        var stringContent = new StringContent(orderRequestModel.ToString());

        httpClient.DefaultRequestHeaders.Add("x-api-key", appSettings.ApiKeyRedVenture);
        HttpResponseMessage responseApi = httpClient.PostAsync($"{appSettings.BaseUrl}/orders/generate-id", stringContent).Result;

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
