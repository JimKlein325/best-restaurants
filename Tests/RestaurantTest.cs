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
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_eats_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameNameIDPhoneNumber()
    {
      Restaurant firstRestaurant = new Restaurant("lardo", 1, "455");
      Restaurant secondRestaurant = new Restaurant("lardo", 1, "455");

      Assert.Equal(firstRestaurant,secondRestaurant);
    }

    [Fact]
    public void Test_GetAll_RestaurantsEmptyAtFirst()
    {
      //Arrange, Act
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_SaveRestaurantstoDB()
    {
      Restaurant testRestaurant = new Restaurant("lardo", 1, "455");
      testRestaurant.Save();

      List<Restaurant> restaurants = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      Assert.Equal(testList,restaurants);

    }
    [Fact]
    public void Test_DeleteAll_DeletesRestaurantsFromDB()
    {
      //Arrange
      Restaurant firstRestaurant = new Restaurant("lardo", 1, "455");
      Restaurant secondRestaurant = new Restaurant("Chaba Thai", 1, "455");
      firstRestaurant.Save();
      secondRestaurant.Save();

      //Act
      Restaurant.DeleteAll();
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.Equal(0,result);
    }

    [Fact]
    public void Test_Find_FindsRestaurantAdded()
    {
      //Arrange
      Restaurant firstRestaurant = new Restaurant("lardo", 0, "455");
      Restaurant secondRestaurant = new Restaurant("Chaba Thai", 1, "455");
      firstRestaurant.Save();
      secondRestaurant.Save();

      //Act
      Restaurant result = Restaurant.Find(secondRestaurant.GetId());

      string nameTest = result.GetName();
      Console.WriteLine("From Find:  " + nameTest);

      string gaName = Restaurant.GetAll()[1].GetName();
      Console.WriteLine("From GetAll:  " + gaName);

      //Assert
      Assert.Equal("Chaba Thai", nameTest);
    }
    [Fact]
    public void Test_Delete_DeletesRestaurantFromDB()
    {
      //Arrange
      //Arrange
      Restaurant firstRestaurant = new Restaurant("lardo", 0, "455");
      Restaurant secondRestaurant = new Restaurant("Chaba Thai", 1, "455");
      firstRestaurant.Save();
      secondRestaurant.Save();
      //Act
      firstRestaurant.Delete();
      List<Restaurant> resultREstaurants = Restaurant.GetAll();
      List<Restaurant> testRestaurantList = new List<Restaurant> {secondRestaurant};



      //Assert
      Assert.Equal(testRestaurantList, resultREstaurants);
    }

    [Fact]
      public void Test_Update_UpdatesRestaurantInDatabase()
      {
        //Arrange
        //Arrange
        Restaurant firstRestaurant = new Restaurant("lardo", 0, "455");
        Restaurant secondRestaurant = new Restaurant("Chaba Thai", 1, "455");
        firstRestaurant.Save();
        secondRestaurant.Save();

        Restaurant result = Restaurant.Find(firstRestaurant.GetId());

        //Act
        result.Update(secondRestaurant.GetName(), secondRestaurant.GetCuisineId(), secondRestaurant.GetPhoneNumber());

        Restaurant updatedResult = Restaurant.Find(firstRestaurant.GetId());

        //Assert
        Assert.Equal(secondRestaurant.GetName(), updatedResult.GetName());
        Assert.Equal(secondRestaurant.GetCuisineId() , updatedResult.GetCuisineId());
        Assert.Equal(secondRestaurant.GetPhoneNumber() , updatedResult.GetPhoneNumber());
      }
    //
    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
  }
}
