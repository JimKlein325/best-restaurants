using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Restaurants
{
  public class Cuisine
  {
    private int _id;
    private string _name;

    public Cuisine(string Name,  int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine ))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        //bool idEquality = this.GetId()  == newCuisine.GetId();
        bool nameEquality = this.GetName() == newCuisine.GetName();
        return nameEquality;
      }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public static List<Cuisine> GetAll()
    {
      List<Cuisine> cuisines =  new List<Cuisine>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        string cuisineName = rdr.GetString(0);
        int cuisineId = rdr.GetInt32(1);
        Cuisine cuisine = new Cuisine(cuisineName, cuisineId);
        cuisines.Add(cuisine);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return cuisines;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO cuisines (name) OUTPUT INSERTED.id VALUES (@CuisineName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@CuisineName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }


    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE cuisines SET name = @NewName OUTPUT INSERTED.name WHERE id = @CategoryId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);


      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@CategoryId";
      categoryIdParameter.Value = this.GetId();
      cmd.Parameters.Add(categoryIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM cuisines WHERE id = @id;", conn);

      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@id";
      categoryIdParameter.Value = this.GetId();

      cmd.Parameters.Add(categoryIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM cuisines;", conn);
      cmd.ExecuteNonQuery();
    }

    public static Cuisine Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines  WHERE id = @CuisineId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@CuisineId";
      idParameter.Value = id.ToString();
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();

      string foundCuisineName = null;
      int foundCuisineId = 0;


      while(rdr.Read())
      {
        foundCuisineName = rdr.GetString(0);
        foundCuisineId = rdr.GetInt32(1);
        Console.WriteLine(foundCuisineId.ToString());
      }
      Cuisine foundCuisine = new Cuisine(
        foundCuisineName,
        foundCuisineId
      );

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCuisine;
    }
    public List<Restaurant> GetRestaurants()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE cuisine_id = @CategoryId;", conn);
      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@CategoryId";
      categoryIdParameter.Value = this.GetId();
      cmd.Parameters.Add(categoryIdParameter);
      rdr = cmd.ExecuteReader();

      List<Restaurant> restaurants = new List<Restaurant> {};
      while(rdr.Read())
      {
        string restaurantPhone = rdr.GetString(2);
        string restaurantName= rdr.GetString(0);
        int restaurantCuisineId = rdr.GetInt32(1);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCuisineId, restaurantPhone);
        restaurants.Add(newRestaurant);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return restaurants;
    }


  }
}
