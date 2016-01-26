using System;
using System.Data.SqlClient;

namespace E_Handel.BL
{
    public class BLProduct
    {
        public int Id { get; private set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Popularity { get; set; }
        public int StockQuantity { get; set; }
        public double VAT { get; set; }
        public double Discount { get; set; }
        public string TrailerUrl { get; set; }

        public BLProduct(int id, int categoryId, string name, string description, double price, int popularity, int stockQuantity, double VAT, double discount = 0, string trailerUrl = null)
        {
            Id = id;
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Price = price;
            Popularity = popularity;
            StockQuantity = stockQuantity;
            this.VAT = VAT;
            Discount = discount;
            TrailerUrl = trailerUrl;
        }
        public BLProduct(string databaseConnectionString, int id)
        {
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            SqlCommand sqlGetProduct = new SqlCommand($"SELECT CategoryID, Name, Description, Price, Popularity, StockQuantity, VAT FROM Products WHERE ID = {id}", sqlConnection);
            SqlDataReader sqlReader = null;
            try
            {
                sqlConnection.Open();
                sqlReader = sqlGetProduct.ExecuteReader();
                while (sqlReader.Read())
                {
                    Id = id;
                    CategoryId = int.Parse(sqlReader["CategoryID"].ToString());
                    Name = sqlReader["Name"].ToString();
                    Description = sqlReader["Description"].ToString();
                    Price = double.Parse(sqlReader["Price"].ToString());
                    Popularity = int.Parse(sqlReader["Popularity"].ToString());
                    StockQuantity = int.Parse(sqlReader["StockQuantity"].ToString());
                    VAT = double.Parse(sqlReader["VAT"].ToString());
                }
                GetDiscountFromDB(databaseConnectionString);
                if (!TryGetTrailerUrlFromDB(databaseConnectionString))
                    TrailerUrl = null;
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
                sqlGetProduct.Dispose();
            }
        }

        public void GetDiscountFromDB(string databaseConnectionString)
        {
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            SqlCommand sqlGetProduct = new SqlCommand($"SELECT DiscountPercentage FROM DiscountedProducts WHERE ProductID = {Id}", sqlConnection);
            SqlDataReader sqlReader = null;
            Discount = 0;
            try
            {
                sqlConnection.Open();
                sqlReader = sqlGetProduct.ExecuteReader();
                while (sqlReader.Read())
                {
                    Discount = double.Parse(sqlReader["DiscountPercentage"].ToString());
                }
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
                sqlGetProduct.Dispose();
            }
        }
        public bool TryGetTrailerUrlFromDB(string databaseConnectionString)
        {
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            SqlCommand sqlGetProduct = new SqlCommand($"SELECT TrailerURL FROM ProductTrailers WHERE ProductID = {Id}", sqlConnection);
            SqlDataReader sqlReader = null;
            try
            {
                sqlConnection.Open();
                sqlReader = sqlGetProduct.ExecuteReader();
                while (sqlReader.Read())
                {
                    TrailerUrl = sqlReader["TrailerURL"].ToString();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
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
                sqlGetProduct.Dispose();
            }
        }

        public override string ToString() => $"Id = {Id}, CategoryId = {CategoryId}, Name = {Name}, Description = {Description}, Price = {Price}, Popularity = {Popularity}, StockQuantity = {StockQuantity}, VAT = {VAT}, Description = {Description}";
    }
}