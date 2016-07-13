using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Restaurants
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_eats_tests;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      Restaurant firstRestaurant = new Restaurant("lardo", 1, "455");
      Restaurant secondRestaurant = new Restaurant("lardo", 1, "455");

      Assert.Equal(firstRestaurant,secondRestaurant);


    }

    // [Fact]
    // public void Test_CategoriesEmptyAtFirst()
    // {
    //   //Arrange, Act
    //   int result = Category.GetAll().Count;
    //
    //   //Assert
    //   Assert.Equal(0, result);
    // }
    //
    public void Dispose()
    {
      // Task.DeleteAll();
      // Category.DeleteAll();
    }
  }
}
