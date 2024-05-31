using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RamenGo.Domain.RequestModel;
using RamenGo.Domain.ResponseModel;
using RamenGo.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamenGO.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  [Consumes("application/json")]


  public class RamenGoController : ControllerBase
  {
    private readonly IIngredientsService _ingredientsService;
    private readonly AppSettings _appSettings;
    private readonly IOrderService _orderService;
    public RamenGoController(AppSettings appSettings, IIngredientsService ingredientsService, IOrderService orderService)
    {
      this._appSettings = appSettings;
      _ingredientsService = ingredientsService;
      _orderService = orderService;
    }
    private bool ApiKeyIsValid(string apiKey)
    {
      if (apiKey != _appSettings.ApiKey)
        return false;
      else
        return true;
    }

    /// <summary>
    /// List all available broths
    /// </summary>
    /// <remarks>
    ///
    /// </remarks>
    /// <returns>A list of broths</returns>
    /// <response code="200">Returns the list of broths</response>
    /// <response code="403">
    /// Forbidden
    /// </response>
    [HttpGet("/broths")]
    public ObjectResult GetBroths([FromHeader(Name = "x-api-key")] string apiKey)
    {
      try
      {
        if (!this.ApiKeyIsValid(apiKey))
          return StatusCode(403, new ErrorResponse { Error = "x-api-key header missing" });

        var listBroths = _ingredientsService.GetBroths();

        if (listBroths != null)
          return new ObjectResult(listBroths) { StatusCode = 200 };
        else
          return StatusCode(403, new ErrorResponse { Error = "x-api-key header missing" });

      }
      catch (Exception ex)
      {
        return StatusCode(403, new ErrorResponse { Error = "x-api-key header missing" });
      }

    }


    /// <summary>
    /// List all available proteins
    /// </summary>
    /// <remarks>
    ///
    /// </remarks>
    /// <returns>A list of proteins</returns>
    /// <response code="200">Returns the list of proteins</response>
    /// <response code="403">
    /// Forbidden
    /// </response>
    [HttpGet("/proteins")]
    public ObjectResult GetProteins([FromHeader(Name = "x-api-key")] string apiKey)
    {
      try
      {

        if (apiKey != _appSettings.ApiKey)
          return StatusCode(403, new ErrorResponse { Error = "x-api-key header missing" });

        var result = _ingredientsService.GetProteins();

        if (result != null)
          return Ok(result);
        else
          return StatusCode(403, new ErrorResponse { Error = "x-api-key header missing" });

      }
      catch (Exception ex)
      {
        return StatusCode(403, new ErrorResponse { Error = "x-api-key header missing" });
      }

    }


    /// <summary>
    /// Place an order
    /// </summary>
    /// <remarks>
    ///
    /// </remarks>
    /// 
    /// <returns>Order details</returns>
    /// 
    /// <response code="201">Order placed successfully</response>
    /// <response code="400">Invalid request</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">Internet server error</response>
    [HttpPost("/orders")]
    public ObjectResult Order([FromBody] OrderRequestModel orderRequestModel, [FromHeader(Name = "x-api-key")] string apiKey)
    {
      try
      {
        if (!this.ApiKeyIsValid(apiKey))
          return StatusCode(403, new ErrorResponse { Error = "x-api-key header missing" });

        if (orderRequestModel.BrothId <= 0 || orderRequestModel.BrothId <= 0)
          return StatusCode(400, new ErrorResponse { Error = "both brothId and proteinId are required" });

        var order = _orderService.MakeOrder(orderRequestModel);
        if (order != null)
          return new ObjectResult(order) { StatusCode = 201 };
        else
          return StatusCode(500, new ErrorResponse { Error = "could not place order" });

      }
      catch (Exception ex)
      {
        return StatusCode(500, new ErrorResponse { Error = "could not place order" });
      }

    }

    /// <summary>
    /// Place an order
    /// </summary>
    /// <remarks>
    ///
    /// </remarks>
    /// 
    /// <returns>Order details</returns>
    /// 
    /// <response code="201">Order placed successfully</response>
    /// <response code="400">Invalid request</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">Internet server error</response>
    [HttpPost("/order")]
    [Consumes("text/plain")]
    [Produces("application/json")]
    public async Task<IActionResult> OrderPlainText([FromHeader(Name = "x-api-key")] string apiKey)
    {
      try
      {
        OrderRequestModel orderRequestModel = null;
        using (StreamReader reader = new StreamReader(Request.Body))
        {
          var bodyText = await reader.ReadToEndAsync();
          if (!string.IsNullOrEmpty(bodyText))
            orderRequestModel = JsonConvert.DeserializeObject<OrderRequestModel>(bodyText);
        }

        if(!ApiKeyIsValid(apiKey))
          return StatusCode(403, new ErrorResponse { Error = "x-api-key header missing" });

        if (orderRequestModel == null)
          return StatusCode(500, new ErrorResponse { Error = "could not place order" });

        if (orderRequestModel.BrothId <= 0 || orderRequestModel.BrothId <= 0)
          return StatusCode(400, new ErrorResponse { Error = "both brothId and proteinId are required" });



        var orderResponse = _orderService.MakeOrder(orderRequestModel);


        if (orderResponse != null)
          return StatusCode(201, orderResponse);
        else
          return StatusCode(500, new ErrorResponse { Error = "could not place order" });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new ErrorResponse { Error = "could not place order" });
      }
    }
    public class ErrorResponse
    {
      public string Error { get; set; }
    }

  }
}
