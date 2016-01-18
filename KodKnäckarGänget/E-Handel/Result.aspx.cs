using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using E_Handel.BL;

namespace E_Handel
{
    public partial class Result : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["KKG-EHandelConnectionString"].ConnectionString;

        private List<BLProduct> resultBLProducts = new List<BLProduct>();
        private string searchString;
        private int categoryId;
        private string categoryName;
        private string categoryDescription;

        protected void Page_Load(object sender, EventArgs e)
        {
            searchString = Request.QueryString["search"];
            string categoryIdString = Request.QueryString["categoryId"];

            if (searchString != null)
            {
                if (ValidateSearchString())
                {
                    LoadSearchResult();
                    DisplaySearchResult();
                }
            }
            else if (categoryIdString != null)
            {
                if (int.TryParse(categoryIdString, out categoryId))
                {
                    LoadCategoryResult();
                    DisplayCategoryResult();
                }
            }
            else
            {
                ResultTitle.InnerHtml = "No search or category results."; //or error page
            }
        }

        private bool ValidateSearchString()
        {
            //TBI
            return true;
        }
        private void LoadSearchResult()
        {
            //TBI
        }
        private void DisplaySearchResult()
        {
            //TBI
            ResultTitle.InnerHtml = "Search results: ";
        }

        private void LoadCategoryResult()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlGetCategory = new SqlCommand($"SELECT Name, Description FROM Categories WHERE ID = {categoryId}", sqlConnection);
            SqlCommand sqlGetProducts = new SqlCommand($"SELECT ID, Name, Price, Popularity, StockQuantity, VAT FROM Products WHERE CategoryID = {categoryId}", sqlConnection);
            SqlDataReader sqlCategoryDataReader = null;
            SqlDataReader sqlProductDataReader = null;
            try
            {
                sqlConnection.Open();

                sqlCategoryDataReader = sqlGetCategory.ExecuteReader();
                while (sqlCategoryDataReader.Read())
                {
                    categoryName = sqlCategoryDataReader["Name"].ToString();
                    categoryDescription = sqlCategoryDataReader["Description"].ToString();
                }
                sqlProductDataReader = sqlGetProducts.ExecuteReader();
                while (sqlProductDataReader.Read())
                {
                    int id = int.Parse(sqlProductDataReader["ID"].ToString());
                    string name = sqlProductDataReader["Name"].ToString();
                    double price = double.Parse(sqlProductDataReader["Price"].ToString());
                    int popularity = int.Parse(sqlProductDataReader["Popularity"].ToString());
                    int stockQuantity = int.Parse(sqlProductDataReader["StockQuantity"].ToString());
                    double VAT = double.Parse(sqlProductDataReader["VAT"].ToString());
                    resultBLProducts.Add(new BLProduct(id, categoryId, name, price, popularity, stockQuantity, VAT));
                }
            }
            catch (Exception ex)
            {
                ResultTitle.InnerHtml = "No search or category results."; //or error page
            }
            finally
            {
                if (sqlCategoryDataReader != null)
                {
                    sqlCategoryDataReader.Close();
                    sqlCategoryDataReader.Dispose();
                }
                if (sqlProductDataReader != null)
                {
                    sqlProductDataReader.Close();
                    sqlProductDataReader.Dispose();
                }
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlGetProducts.Dispose();
            }
        }
        private void DisplayCategoryResult()
        {
            //TBI
            ResultTitle.InnerHtml = categoryName;
        }
    }
}