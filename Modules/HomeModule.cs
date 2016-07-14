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

      Get["/cuisines"] =_=>
      {
        return View["cuisines.cshtml",Cuisine.GetAll()];
      };

      Get["cuisines/restaurants/{id}"] = paramaters => {
        Cuisine cuisine = Cuisine.Find(paramaters.id);
        return View["restaurants.cshtml", cuisine.GetRestaurants()];
      };
      Get["cuisines/delete/{id}"] = parameters => {
        Cuisine  cuisine = Cuisine.Find(parameters.id);
        return View["confirm_delete_cuisine.cshtml", cuisine];
      };

      Delete["cuisines/delete/{id}"] = parameters => {
        Cuisine  cuisine = Cuisine.Find(parameters.id);
        cuisine.Delete();
        return View["cuisines.cshtml", Cuisine.GetAll()];
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


      Get["cuisine/edit/{id}"] = parameters => {
        Cuisine editCuisine = Cuisine.Find(parameters.id);
        return View["cuisinie_edit.cshtml",editCuisine];
      };

      Patch["/cuisine/edit/{id}"] = parameters => {
        Cuisine editCuisine = Cuisine.Find(parameters.id);
        editCuisine.Update(Request.Form["cuisine-name"]);
        return View["cuisines.cshtml",Cuisine.GetAll()];
      };


      Get["restaurants/{id}"] = parameters => {
        Restaurant restaurant = Restaurant.Find(parameters.id);
        return View["restaurant_detail.cshtml", restaurant];
      };

      Get["restaurants/edit/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Restaurant restaurant = Restaurant.Find(parameters.id);
        List<Cuisine> allCuisines = Cuisine.GetAll();
        model.Add("restaurant", restaurant);
        model.Add("cuisines", allCuisines);
        return View["restaurants_edit.cshtml", model];
      };

      Patch["restaurants/edit/{id}"] = parameters => {
        Restaurant restaurant = Restaurant.Find(parameters.id);
        restaurant.Update(
          Request.Form["restaurant-name"],
          Request.Form["category-id"],
          Request.Form["restaurant-phoneNumber"]
        );
        return View["restaurants.cshtml", Restaurant.GetAll()];
      };

      Get["restaurants/delete/{id}"] = parameters => {
        Restaurant restaurant = Restaurant.Find(parameters.id);
        return View["confirm_delete_restaurant.cshtml", restaurant];
      };

      Delete["restaurants/delete/{id}"] = parameters => {
        Restaurant restaurant = Restaurant.Find(parameters.id);
        restaurant.Delete();
        return View["restaurants.cshtml", Restaurant.GetAll()];
      };
      Delete["/restuarants/clearall"] =_=>{
        Restaurant.DeleteAll();
        return View["cleared.cshtml"];
      };

    }
  }
}
