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
      Get["cuisines/new"] =_=>{
        return View["cuisines_form.cshtml"];

      };



      Post["cuisines/new"] =_=>{
        string name = Request.Form["cuisine-name"];
        Cuisine newCuisine = new Cuisine(name);
        newCuisine.Save();
        return View["cuisines.cshtml", Cuisine.GetAll()];

      };
      Get["restaurants/"] = _ => {
        return View["restaurants.cshtml", Restaurant.GetAll()];
      };

      Get["restaurants/new"] =_=>{

        return View["restaurants_form.cshtml", Cuisine.GetAll()];
      };

      Post["restaurants/new"] =_=>{
        string name = Request.Form["cuisine-name"];
        Restaurant  newRestaurant = new Restaurant(
          Request.Form["restaurant-name"],
          Request.Form["category-id"],
          Request.Form["restaurant-phoneNumber"]
        );
        newRestaurant.Save();
        return View["restaurants.cshtml", Restaurant.GetAll()];

      };
      Get["restaurants/{id}"] = parameters => {
        Restaurant restaurant = Restaurant.Find(parameters.id);
        return View["restaurant_detail.cshtml", restaurant];
      };

      Get["restaurants/edit/{id}"] = parameters => {
        Restaurant restaurant = Restaurant.Find(parameters.id);
        return View["restaurants_edit.cshtml", restaurant];
      };

    }
  }
}
