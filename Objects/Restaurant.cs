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

    public Restaurant(string Name, int Id = 0, int cuisineID, string phoneNumber)
    {
      _id = Id;
      _name = Name;
      _cuisine_id = cuisineID;
      _phoneNumber = phoneNumber;

    }

    public override bool Equals(System.Object otherCategory)
    {

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
    public static List<Category> GetAll()
    {

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

    public void Save()
    {

    }
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
    // public void Delete()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("DELETE FROM categories WHERE id = @CategoryId; DELETE FROM tasks WHERE category_id = @CategoryId;", conn);
    //
    //   SqlParameter categoryIdParameter = new SqlParameter();
    //   categoryIdParameter.ParameterName = "@CategoryId";
    //   categoryIdParameter.Value = this.GetId();
    //
    //   cmd.Parameters.Add(categoryIdParameter);
    //   cmd.ExecuteNonQuery();
    //
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    // }
    // public static void DeleteAll()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //   SqlCommand cmd = new SqlCommand("DELETE FROM categories;", conn);
    //   cmd.ExecuteNonQuery();
    // }
    //
    // public static Category Find(int id)
    // {
    //   SqlConnection conn = DB.Connection();
    //   SqlDataReader rdr = null;
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("SELECT * FROM categories WHERE id = @CategoryId;", conn);
    //   SqlParameter categoryIdParameter = new SqlParameter();
    //   categoryIdParameter.ParameterName = "@CategoryId";
    //   categoryIdParameter.Value = id.ToString();
    //   cmd.Parameters.Add(categoryIdParameter);
    //   rdr = cmd.ExecuteReader();
    //
    //   int foundCategoryId = 0;
    //   string foundCategoryDescription = null;
    //
    //   while(rdr.Read())
    //   {
    //     foundCategoryId = rdr.GetInt32(0);
    //     foundCategoryDescription = rdr.GetString(1);
    //   }
    //   Category foundCategory = new Category(foundCategoryDescription, foundCategoryId);
    //
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    //   return foundCategory;
    // }
  }
}
