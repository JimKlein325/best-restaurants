using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Restaurants
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"]= _ => {
        // List<Category> AllCategories = Category.GetAll();
        return View["index.cshtml"];
      };


    }
  }
}
