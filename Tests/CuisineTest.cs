using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Restaurants
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_eats_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameNameForCuisine()
    {
      Cuisine firstCuisine = new Cuisine("lardo");
      Cuisine secondCuisine = new Cuisine("lardo");

      Assert.Equal(firstCuisine,secondCuisine);
    }

    [Fact]
    public void Test_GetAll_CuisinesEmptyAtFirst()
    {
      //Arrange, Act
      int result = Cuisine.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_SaveCuisinestoDB()
    {
      Cuisine testCuisine = new Cuisine("Bavarian");
      testCuisine.Save();

      List<Cuisine> cuisines = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};

      Assert.Equal(testList,cuisines);

    }
    [Fact]
    public void Test_DeleteAll_DeletesCuisinesFromDB()
    {
      //Arrange
      Cuisine firstCuisine = new Cuisine("Greek");
      Cuisine secondCuisine = new Cuisine("TexMex");
      firstCuisine.Save();
      secondCuisine.Save();

      //Act
      Cuisine.DeleteAll();
      int result = Cuisine.GetAll().Count;

      //Assert
      Assert.Equal(0,result);
    }

    [Fact]
    public void Test_Find_FindsCuisineAdded()
    {
      //Arrange
      Cuisine firstCuisine = new Cuisine("Greek");
      Cuisine secondCuisine = new Cuisine("TexMex");
      firstCuisine.Save();
      secondCuisine.Save();

      //Act
      Cuisine result = Cuisine.Find(secondCuisine.GetId());

      string nameTest = result.GetName();
      Console.WriteLine("From Find:  " + nameTest);

      string gaName = Cuisine.GetAll()[1].GetName();
      Console.WriteLine("From GetAll:  " + gaName);

      //Assert
      Assert.Equal("TexMex", nameTest);
    }

    [Fact]
    public void Test_Delete_DeletesCuisineFromDB()
    {
      //Arrange
      //Arrange
      Cuisine firstCuisine = new Cuisine("lardo");
      Cuisine secondCuisine = new Cuisine("Chaba Thai");
      firstCuisine.Save();
      secondCuisine.Save();
      //Act
      firstCuisine.Delete();
      List<Cuisine> resultREstaurants = Cuisine.GetAll();
      List<Cuisine> testCuisineList = new List<Cuisine> {secondCuisine};



      //Assert
      Assert.Equal(testCuisineList, resultREstaurants);
    }

    [Fact]
      public void Test_Update_UpdatesCuisineInDatabase()
      {
        //Arrange
        //Arrange
        Cuisine firstCuisine = new Cuisine("lardo");
        Cuisine secondCuisine = new Cuisine("Chaba Thai");
        firstCuisine.Save();
        secondCuisine.Save();

        Cuisine result = Cuisine.Find(firstCuisine.GetId());

        //Act
        result.Update(secondCuisine.GetName());

        Cuisine updatedResult = Cuisine.Find(firstCuisine.GetId());

        //Assert
        Assert.Equal(secondCuisine.GetName(), updatedResult.GetName());

      }
      [Fact]
      public void Test_GetRestaurants_RetrievesAllRestaurantssWithCuisine()
      {
        //Arrange
        Cuisine testCuisine = new Cuisine("texmex");
        testCuisine.Save();

        //Act
        Restaurant firstRestaurant = new Restaurant("lardo", testCuisine.GetId(), "455");
        Restaurant secondRestaurant = new Restaurant("Chaba Thai", testCuisine.GetId(), "455");
        firstRestaurant.Save();
        secondRestaurant.Save();

        List<Restaurant> testRestaurantList = new List<Restaurant> {firstRestaurant, secondRestaurant};
        List<Restaurant> resultRestaurantList = testCuisine.GetRestaurants();

        //Assert
        Assert.Equal(testRestaurantList[0].GetCuisineId(), resultRestaurantList[0].GetCuisineId());
      }


    public void Dispose()
    {
      Cuisine.DeleteAll();
    }
  }
}
