using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RamenGo.ACL.Interfaces;
using RamenGo.ACL.Rest;
using RamenGo.Domain.RequestModel;
using RamenGo.Domain.ResponseModel;
using RamenGo.Service;
using RamenGo.Service.Interfaces;
using RamenGO.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamenGo.Test
{
  public class OrderAppServiceTest
  {

    private IOrderService _orderService;

    public IConfiguration Configuration { get; }
    public IConfiguration InitConfiguration()
    {
      var config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
          .Build();
      return config;
    }

      [SetUp]
    public void Setup()
    {
      var configuration = InitConfiguration();

      var appSettingsSection = configuration.GetSection("AppSettings");

      var _appSettings = appSettingsSection.Get<AppSettings>();

      var serviceCollection = new ServiceCollection();

      serviceCollection.AddTransient<IOrderService, OrderService>();
      serviceCollection.AddTransient<IRedventureAPI, RedventureAPI>();


      var serviceProvider = serviceCollection.BuildServiceProvider();
      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
      var orderService = serviceProvider.GetService<IOrderService>();
      var redventureAPI = serviceProvider.GetService<IRedventureAPI>();

      _orderService = new OrderService(redventureAPI);
    }
    [Test]
    public void TestMakeOrder()
    {
      OrderResponseModel orderResponseModel = null;

      var orderRequestModel = CreateModel();

      try
      {
        orderResponseModel = _orderService.MakeOrder(orderRequestModel);
      }
      catch (Exception ex)
      {
        Assert.Fail("Falha ao executar o pedido: " + ex);
      }

      if (orderResponseModel != null)
        Assert.Pass("Pedido feito com sucesso!");
      else
        Assert.Fail("Falha ao executar o pedido");
    }

    OrderRequestModel CreateModel()
    {
      return new OrderRequestModel()
      {
        BrothId = 1,
        ProteinId = 2
      };
    }
  }
}
