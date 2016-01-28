using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace E_Handel.BL
{
    public class BLCategory
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public BLCategory(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public static BLCategory RetrieveFromDB(string databaseConnectionString, int id)
        {
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            SqlCommand sqlGetCategory = new SqlCommand($"SELECT Name, Description FROM Categories WHERE ID = {id}", sqlConnection);
            SqlDataReader sqlReader = null;
            try
            {
                sqlConnection.Open();
                sqlReader = sqlGetCategory.ExecuteReader();
                while (sqlReader.Read())
                    return new BLCategory(id: id, name: sqlReader["Name"].ToString(),
                        description: sqlReader["Description"].ToString());
                return null;
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                    sqlReader.Dispose();
                }
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlGetCategory.Dispose();
            }
        }
        public static List<BLCategory> RetrieveListFromDB(string databaseConnectionString)
        {
            List<BLCategory> categories = new List<BLCategory>();
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            SqlCommand sqlGetCategories = new SqlCommand("SELECT ID, Name, Description FROM Categories", sqlConnection);
            SqlDataReader sqlReader = null;
            try
            {
                sqlConnection.Open();
                sqlReader = sqlGetCategories.ExecuteReader();
                while (sqlReader.Read())
                    categories.Add(new BLCategory(id: int.Parse(sqlReader["ID"].ToString()),
                        name: sqlReader["Name"].ToString(), description: sqlReader["Description"].ToString()));
                return categories;
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                    sqlReader.Dispose();
                }
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlGetCategories.Dispose();
            }
        }

        public override string ToString() => $"Id = {Id}, Name = {Name}, Description = {Description}";
    }
}