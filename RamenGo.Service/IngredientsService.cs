using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RamenGo.Domain.ResponseModel;
using RamenGo.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamenGo.Service
{
  public class IngredientsService : IIngredientsService
  {

    public List<BrothModel> GetBroths()
    {
      List<BrothModel> listBroth;

      listBroth = GetMockDataBroth();

      return listBroth;
    }
    public List<ProteinsModel> GetProteins()
    {
      List<ProteinsModel> listProteins = new List<ProteinsModel>();

      var firstProteins = new ProteinsModel()
      {
        Id = "1",
        ImageInactive = "https://tech.redventures.com.br/icons/pork/inactive.svg",
        ImageActive = "https://tech.redventures.com.br/icons/pork/active.svg",
        Name = "Chasu",
        Description = "A sliced flavourful pork meat with a selection of season vegetables.",
        Price = 10
      };
      var secondProteins = new ProteinsModel()
      {
        Id = "2",
        ImageInactive = "https://tech.redventures.com.br/icons/pork/inactive.svg",
        ImageActive = "https://tech.redventures.com.br/icons/pork/active.svg",
        Name = "Meat",
        Description = "Não é vegano",
        Price = 15
      };
      listProteins.Add(firstProteins);
      listProteins.Add(secondProteins);
      return listProteins;
    }
    private List<BrothModel> GetMockDataBroth()
    {
      List<BrothModel> listBroth = new List<BrothModel>();

      var firstList = new BrothModel()
      {
        Id = "1",
        ImageInactive = "https://tech.redventures.com.br/icons/salt/inactive.svg",
        ImageActive = "https://tech.redventures.com.br/icons/salt/active.svg",
        Name = "Salt",
        Description = "Simple like the seawater, nothing more",
        Price = 10
      };
      var secondList = new BrothModel()
      {
        Id = "2",
        ImageInactive = "https://tech.redventures.com.br/icons/pork/inactive.svg",
        ImageActive = "https://tech.redventures.com.br/icons/pork/active.svg",
        Name = "Pepper",
        Description = "It's very hot, good luck!",
        Price = 5
      };
      listBroth.Add(firstList);
      listBroth.Add(secondList);
      return listBroth;
    }


    private List<ProteinsModel> GetMockDataProtein()
    {
      List<ProteinsModel> listProtein = new List<ProteinsModel>();

      var firstList = new ProteinsModel()
      {
        Id = "1",
        ImageInactive = "https://tech.redventures.com.br/icons/pork/inactive.svg",
        ImageActive = "https://tech.redventures.com.br/icons/pork/active.svg",
        Name = "Chasu",
        Description = "A sliced flavourful pork meat with a selection of season vegetables.",
        Price = 10
      };
      var secondList = new ProteinsModel()
      {
        Id = "2",
        ImageInactive = "https://tech.redventures.com.br/icons/salt/inactive.svg",
        ImageActive = "https://tech.redventures.com.br/icons/salt/active.svg",
        Name = "Meat",
        Description = "A sliced cow meat",
        Price = 5
      };
      listProtein.Add(firstList);
      listProtein.Add(secondList);
      return listProtein;
    }
  }
}
