using System.Collections.Generic;
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

        public static BLProduct RetrieveFromDB(string databaseConnectionString, int id)
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
                    BLProduct product = new BLProduct(id: id, categoryId: int.Parse(sqlReader["CategoryID"].ToString()),
                        name: sqlReader["Name"].ToString(), description: sqlReader["Description"].ToString(),
                        price: double.Parse(sqlReader["Price"].ToString()),
                        popularity: int.Parse(sqlReader["Popularity"].ToString()),
                        stockQuantity: int.Parse(sqlReader["StockQuantity"].ToString()),
                        VAT: double.Parse(sqlReader["VAT"].ToString()));
                    product.GetDiscountFromDB(databaseConnectionString);
                    product.GetTrailerUrlFromDB(databaseConnectionString);
                    return product;
                }
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
                sqlGetProduct.Dispose();
            }
        }
        public static List<BLProduct> RetrieveListFromDB(string databaseConnectionString, string sqlConditionString)
        {
            List<BLProduct> products = new List<BLProduct>();
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            SqlCommand sqlGetProducts = new SqlCommand($"SELECT ID, CategoryID, Name, Description, Price, Popularity, StockQuantity, VAT FROM Products WHERE {sqlConditionString}", sqlConnection);
            SqlDataReader sqlReader = null;
            try
            {
                sqlConnection.Open();

                sqlReader = sqlGetProducts.ExecuteReader();
                while (sqlReader.Read())
                {
                    BLProduct product = new BLProduct(id: int.Parse(sqlReader["ID"].ToString()), categoryId: int.Parse(sqlReader["CategoryID"].ToString()),
                        name: sqlReader["Name"].ToString(), description: sqlReader["Description"].ToString(),
                        price: double.Parse(sqlReader["Price"].ToString()),
                        popularity: int.Parse(sqlReader["Popularity"].ToString()),
                        stockQuantity: int.Parse(sqlReader["StockQuantity"].ToString()),
                        VAT: double.Parse(sqlReader["VAT"].ToString()));
                    product.GetDiscountFromDB(databaseConnectionString);
                    product.GetTrailerUrlFromDB(databaseConnectionString);
                    products.Add(product);
                }
                return products;
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
                sqlGetProducts.Dispose();
            }
        }
        public static List<BLProduct> RetrieveDiscountedProductsFromDB(string databaseConnectionString)
        {
            List<BLProduct> products = new List<BLProduct>();
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            SqlCommand sqlGetDiscounts = new SqlCommand("SELECT ProductID FROM DiscountedProducts", sqlConnection);
            SqlDataReader sqlDiscountDataReader = null;
            try
            {
                sqlConnection.Open();
                sqlDiscountDataReader = sqlGetDiscounts.ExecuteReader();
                while (sqlDiscountDataReader.Read())
                    products.Add(RetrieveFromDB(databaseConnectionString,
                        int.Parse(sqlDiscountDataReader["ProductID"].ToString())));
                return products;
            }
            finally
            {
                if (sqlDiscountDataReader != null)
                {
                    sqlDiscountDataReader.Close();
                    sqlDiscountDataReader.Dispose();
                }
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlGetDiscounts.Dispose();
            }
        }
        public static List<BLProduct> RetrieveTopNPopularProductsFromDB(string databaseConnectionString,
            int numberOfProducts)
        {
            List<BLProduct> products = new List<BLProduct>();
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            SqlCommand sqlGetPopularProducts = new SqlCommand($"SELECT TOP {numberOfProducts} ID, CategoryID, Name, Description, Price, Popularity, StockQuantity, VAT FROM Products ORDER BY Popularity DESC", sqlConnection);
            SqlDataReader sqlReader = null;
            try
            {
                sqlConnection.Open();

                sqlReader = sqlGetPopularProducts.ExecuteReader();
                while (sqlReader.Read())
                {
                    BLProduct product = new BLProduct(id: int.Parse(sqlReader["ID"].ToString()), categoryId: int.Parse(sqlReader["CategoryID"].ToString()),
                        name: sqlReader["Name"].ToString(), description: sqlReader["Description"].ToString(),
                        price: double.Parse(sqlReader["Price"].ToString()),
                        popularity: int.Parse(sqlReader["Popularity"].ToString()),
                        stockQuantity: int.Parse(sqlReader["StockQuantity"].ToString()),
                        VAT: double.Parse(sqlReader["VAT"].ToString()));
                    product.GetDiscountFromDB(databaseConnectionString);
                    product.GetTrailerUrlFromDB(databaseConnectionString);
                    products.Add(product);
                }
                return products;
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
                sqlGetPopularProducts.Dispose();
            }
        }
        public static List<BLProduct> RetrieveVariantsOfProductFromDB(string databaseConnectionString, int productId)
        {
            List<BLProduct> variants = new List<BLProduct>();
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            SqlDataReader sqlReader = null;
            try
            {
                sqlConnection.Open();

                SqlCommand sqlCheckForVariants = new SqlCommand($"SELECT VariantID FROM ProductVariants WHERE ProductID = {productId}", sqlConnection);
                sqlReader = sqlCheckForVariants.ExecuteReader();
                while (sqlReader.Read())
                    variants.Add(RetrieveFromDB(databaseConnectionString, int.Parse(sqlReader["VariantID"].ToString())));
                sqlReader.Close();
                sqlReader.Dispose();

                SqlCommand sqlCheckForOriginal = new SqlCommand($"SELECT ProductID FROM ProductVariants WHERE VariantID = {productId}", sqlConnection);
                bool isNotOriginal = false;
                sqlReader = sqlCheckForOriginal.ExecuteReader();
                while (sqlReader.Read())
                {
                    variants.Add(RetrieveFromDB(databaseConnectionString, int.Parse(sqlReader["ProductID"].ToString())));
                    isNotOriginal = true;
                }
                sqlReader.Close();
                sqlReader.Dispose();

                if (isNotOriginal)
                {
                    SqlCommand sqlCheckForOriginalVariants = new SqlCommand($"SELECT VariantID FROM ProductVariants WHERE ProductID = {variants[0].Id}", sqlConnection);
                    sqlReader = sqlCheckForOriginalVariants.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        if (int.Parse(sqlReader["VariantID"].ToString()) != productId)
                            variants.Add(RetrieveFromDB(databaseConnectionString, int.Parse(sqlReader["VariantID"].ToString())));
                    }
                    sqlCheckForOriginalVariants.Dispose();
                }
                sqlCheckForVariants.Dispose();
                sqlCheckForOriginal.Dispose();
                return variants;
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
                    Discount = double.Parse(sqlReader["DiscountPercentage"].ToString());
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
        public void GetTrailerUrlFromDB(string databaseConnectionString)
        {
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            SqlCommand sqlGetProduct = new SqlCommand($"SELECT TrailerURL FROM ProductTrailers WHERE ProductID = {Id}", sqlConnection);
            SqlDataReader sqlReader = null;
            try
            {
                sqlConnection.Open();
                sqlReader = sqlGetProduct.ExecuteReader();
                while (sqlReader.Read())
                    TrailerUrl = sqlReader["TrailerURL"].ToString();
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