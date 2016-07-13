using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Restaurants
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private int _cuisine_id;
    private string _phoneNumber;

    public Restaurant(string Name,  int cuisineID, string phoneNumber, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _cuisine_id = cuisineID;
      _phoneNumber = phoneNumber;

    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant ))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = this.GetId()  == newRestaurant.GetId();
        bool nameEquality = this.GetName() == newRestaurant.GetName();
        bool phoneEquality = this.GetPhoneNumber() == newRestaurant.GetPhoneNumber();
        return (idEquality && nameEquality && phoneEquality);
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
    public int GetCuisineId()
    {
      return _cuisine_id;
    }
    public string GetPhoneNumber()
    {
      return _phoneNumber;
    }
    public void SetCuisineID(int cuisineID)
    {
      _cuisine_id = cuisineID;
    }
    public void SetPhoneNumber(string newNumber)
    {
      _phoneNumber = newNumber;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public static List<Restaurant> GetAll()
    {
      List<Restaurant> restaurants =  new List<Restaurant>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        string restaurantName = rdr.GetString(0);
        int restaurantId = rdr.GetInt32(3);
        int cuisineID = rdr.GetInt32(1);
        string phoneNumber = rdr.GetString(2);
        Restaurant restaurant = new Restaurant(restaurantName, cuisineID, phoneNumber, restaurantId);
        restaurants.Add(restaurant);
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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name,cuisine_id, phone_number) OUTPUT INSERTED.id VALUES (@RestaurantName, @CuisineID, @PhoneNumber);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@RestaurantName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@CuisineID";
      cuisineIdParameter.Value = this.GetCuisineId();
      cmd.Parameters.Add(cuisineIdParameter);

      SqlParameter phoneNumberParameter= new SqlParameter();
      phoneNumberParameter.ParameterName = "@PhoneNumber";
      phoneNumberParameter.Value = this.GetPhoneNumber();
      cmd.Parameters.Add(phoneNumberParameter);

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


    // public List<Task> GetTasks()
    // {
    //   SqlConnection conn = DB.Connection();
    //   SqlDataReader rdr = null;
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("SELECT * FROM tasks WHERE category_id = @CategoryId;", conn);
    //   SqlParameter categoryIdParameter = new SqlParameter();
    //   categoryIdParameter.ParameterName = "@CategoryId";
    //   categoryIdParameter.Value = this.GetId();
    //   cmd.Parameters.Add(categoryIdParameter);
    //   rdr = cmd.ExecuteReader();
    //
    //   List<Task> tasks = new List<Task> {};
    //   while(rdr.Read())
    //   {
    //     int taskId = rdr.GetInt32(0);
    //     string taskDescription = rdr.GetString(1);
    //     int taskCategoryId = rdr.GetInt32(2);
    //     Task newTask = new Task(taskDescription, taskCategoryId, taskId);
    //     tasks.Add(newTask);
    //   }
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    //   return tasks;
    // }


    // public void Update(string newName)
    // {
    //   SqlConnection conn = DB.Connection();
    //   SqlDataReader rdr;
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("UPDATE categories SET name = @NewName OUTPUT INSERTED.name WHERE id = @CategoryId;", conn);
    //
    //   SqlParameter newNameParameter = new SqlParameter();
    //   newNameParameter.ParameterName = "@NewName";
    //   newNameParameter.Value = newName;
    //   cmd.Parameters.Add(newNameParameter);
    //
    //
    //   SqlParameter categoryIdParameter = new SqlParameter();
    //   categoryIdParameter.ParameterName = "@CategoryId";
    //   categoryIdParameter.Value = this.GetId();
    //   cmd.Parameters.Add(categoryIdParameter);
    //   rdr = cmd.ExecuteReader();
    //
    //   while(rdr.Read())
    //   {
    //     this._name = rdr.GetString(0);
    //   }
    //
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    // }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants WHERE id = @id;", conn);

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
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
    }

    public static Restaurant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants  WHERE id = @RestaurantId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@RestaurantId";
      idParameter.Value = id.ToString();
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();

      string foundRestaurantName = null;
      int foundRestaurantCuisineId = 0;
      string foundRestaurantPhoneNumber = null;
      int foundRestaurantId = 0;


      while(rdr.Read())
      {
        foundRestaurantName = rdr.GetString(0);
        Console.WriteLine("hello");
        foundRestaurantCuisineId = rdr.GetInt32(1);
        foundRestaurantPhoneNumber = rdr.GetString(2);
        foundRestaurantId = rdr.GetInt32(3);
        Console.WriteLine(foundRestaurantId.ToString());
      }
      Restaurant foundRestaurant = new Restaurant(
        foundRestaurantName,
        foundRestaurantCuisineId,
        foundRestaurantPhoneNumber,
        foundRestaurantId
      );

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundRestaurant;
    }
  }
}
