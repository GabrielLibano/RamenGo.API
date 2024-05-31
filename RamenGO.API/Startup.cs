using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RamenGo.Service;
using RamenGo.Service.Interfaces;
using RamenGo.ACL.Interfaces;
using RamenGo.ACL.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.ResponseCompression;

namespace RamenGO.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddScoped<IOrderService, OrderService>();
      services.AddScoped<IIngredientsService, IngredientsService>();

      //ACL
      services.AddScoped<IRedventureAPI, RedventureAPI>();
   
      services.AddControllers().AddJsonOptions(options =>
      {
        //options.JsonSerializerOptions.PropertyNamingPolicy = null;
      });

      services.AddCors(options =>
      {
        options.AddPolicy("AllowAll",
        builder =>
        {
          builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("x-api-key");
        });
      });
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "RamenGO.API", Version = "v1", Contact = new OpenApiContact { Email = "gabriellibanoanselmo@hotmail.com", Name = "Gabriel Libano" } });
        var xmlApiPath = ConfigurationPath.Combine(AppContext.BaseDirectory, "RamenGo.API.xml");  //$"{typeof(Startup).Assembly.GetName().Name}.xml"
        int lastIndex = xmlApiPath.LastIndexOf(':');

        if (lastIndex != -1 && xmlApiPath.Length > 3)
          xmlApiPath = xmlApiPath.Remove(lastIndex, 1);

        c.IncludeXmlComments(xmlApiPath);
      });
      var appSettingsSection = Configuration.GetSection("AppSettings");

      var appSettings = appSettingsSection.Get<AppSettings>();
      services.AddSingleton(appSettings);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
          c.RoutePrefix = "swagger";
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "RamenGO.API v1");
        });
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();
      app.UseCors("AllowAll");
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
