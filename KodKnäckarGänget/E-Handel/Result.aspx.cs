using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Caching;
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
            if (true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void LoadSearchResult()
        {
            string[] searchWords = searchString.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            string sqlSearchString = "%";
            for (int i = 0; i < searchWords.Length; i++)
                sqlSearchString += searchWords[i]+"%";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlGetSearch = new SqlCommand($"SELECT ID, Name, Description, Price, Popularity, StockQuantity, VAT FROM Products WHERE Name LIKE '{sqlSearchString}'", sqlConnection);
            SqlDataReader sqlProductDataReader = null;
            try
            {
                sqlConnection.Open();

                sqlProductDataReader = sqlGetSearch.ExecuteReader();
                while (sqlProductDataReader.Read())
                {
                    int id = int.Parse(sqlProductDataReader["ID"].ToString());
                    string name = sqlProductDataReader["Name"].ToString();
                    string description = sqlProductDataReader["Description"].ToString();
                    double price = double.Parse(sqlProductDataReader["Price"].ToString());
                    int popularity = int.Parse(sqlProductDataReader["Popularity"].ToString());
                    int stockQuantity = int.Parse(sqlProductDataReader["StockQuantity"].ToString());
                    double VAT = double.Parse(sqlProductDataReader["VAT"].ToString());
                    resultBLProducts.Add(new BLProduct(id, categoryId, name, description, price, popularity, stockQuantity, VAT));
                }
            }
            catch (Exception ex)
            {
                ResultTitle.InnerHtml = "Error when attempting to search."; //or error page
            }
            finally
            {
                if (sqlProductDataReader != null)
                {
                    sqlProductDataReader.Close();
                    sqlProductDataReader.Dispose();
                }
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlGetSearch.Dispose();
            }
        }
        private void DisplaySearchResult()
        {
            ResultTitle.InnerHtml = $"Search results for '{searchString}':";
            DisplayProducts();
        }

        private void LoadCategoryResult()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlGetCategory = new SqlCommand($"SELECT Name, Description FROM Categories WHERE ID = {categoryId}", sqlConnection);
            SqlCommand sqlGetProducts = new SqlCommand($"SELECT ID, Name, Description, Price, Popularity, StockQuantity, VAT FROM Products WHERE CategoryID = {categoryId} AND ID NOT IN (SELECT VariantID FROM ProductVariants)", sqlConnection);
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
                sqlCategoryDataReader.Close();
                sqlCategoryDataReader.Dispose();

                sqlProductDataReader = sqlGetProducts.ExecuteReader();
                while (sqlProductDataReader.Read())
                {
                    int id = int.Parse(sqlProductDataReader["ID"].ToString());
                    string name = sqlProductDataReader["Name"].ToString();
                    string description = sqlProductDataReader["Description"].ToString();
                    double price = double.Parse(sqlProductDataReader["Price"].ToString());
                    int popularity = int.Parse(sqlProductDataReader["Popularity"].ToString());
                    int stockQuantity = int.Parse(sqlProductDataReader["StockQuantity"].ToString());
                    double VAT = double.Parse(sqlProductDataReader["VAT"].ToString());
                    resultBLProducts.Add(new BLProduct(id, categoryId, name, description, price, popularity, stockQuantity, VAT));
                }
            }
            catch (Exception ex)
            {
                ResultTitle.InnerHtml = "Error when attempting to retrieve category products."; //or error page
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
                sqlGetCategory.Dispose();
                sqlGetProducts.Dispose();
            }
        }
        private void DisplayCategoryResult()
        {
            ResultTitle.InnerHtml = categoryName;
            ResultImage.Src = $"ImgHandler.ashx?categoryId={categoryId}";
            ResultDescription.InnerHtml = categoryDescription;
            DisplayProducts();
        }

        private void DisplayProducts()
        {
            int currentColumn = 0;
            const int LAST_COLUMN = 3;
            Panel[] tempPanelArray = new Panel[LAST_COLUMN + 1];

            foreach (BLProduct product in resultBLProducts)
            {
                Panel productPanel = new Panel();
                productPanel.CssClass = "span3 result_product_container";

                Image productThumb = new Image();
                productThumb.CssClass = "image_thumbnail";
                productThumb.ImageUrl = $"ImgHandler.ashx?productIdThumb={product.Id}";
                Label productNameLabel = new Label();
                productNameLabel.CssClass = "result_product_name";
                productNameLabel.Text = product.Name;
                Label productPriceLabel = new Label();
                productPriceLabel.CssClass = "result_product_price";
                productPriceLabel.Text = "£"+product.Price;
                Button productInfoButton = new Button();
                productInfoButton.CssClass = "result_product_infobutton";
                productInfoButton.Text = "More info";
                productInfoButton.PostBackUrl = $"Product.aspx?productId={product.Id}";

                productPanel.Controls.Add(productThumb);
                productPanel.Controls.Add(productNameLabel);
                productPanel.Controls.Add(productPriceLabel);
                productPanel.Controls.Add(productInfoButton);

                tempPanelArray[currentColumn] = productPanel;
                currentColumn++;
                if (currentColumn > LAST_COLUMN)
                {
                    Panel productRowPanel = new Panel();
                    Panel productSpanPanel = new Panel();
                    productRowPanel.CssClass = "row-fluid";
                    productSpanPanel.CssClass = "span12";

                    for (int i = 0; i <= LAST_COLUMN; i++)
                        productSpanPanel.Controls.Add(tempPanelArray[i]);

                    productRowPanel.Controls.Add(productSpanPanel);
                    ResultPanel.Controls.Add(productRowPanel);
                    currentColumn = 0;
                }
            }
            if (currentColumn > 0)
            {
                Panel productRowPanel = new Panel();
                Panel productSpanPanel = new Panel();
                productRowPanel.CssClass = "row-fluid";
                productSpanPanel.CssClass = "span12";

                for (int i = 0; i < currentColumn; i++)
                    productSpanPanel.Controls.Add(tempPanelArray[i]);

                productRowPanel.Controls.Add(productSpanPanel);
                ResultPanel.Controls.Add(productRowPanel);
            }
        }
    }
}