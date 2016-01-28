using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            searchString = Request.QueryString["search"];
            string categoryIdString = Request.QueryString["categoryId"];

            if (searchString != null)
            {
                if (ValidateSearchString())
                    DisplaySearchResult();
                else
                    throw new HttpException(404,
                        $"Result.aspx?search={Request.QueryString["search"]} isn't a valid search.");
            }
            else if (categoryIdString != null)
            {
                if (int.TryParse(categoryIdString, out categoryId))
                    DisplayCategoryResult();
                else
                    throw new HttpException(404,
                        $"Result.aspx?categoryId={Request.QueryString["categoryId"]} doesn't exist.");
            }
            else if (Request.QueryString["sales"] != null)
                DisplayDiscountResults();
            else
                throw new HttpException(404, "Result.aspx content doesn't exist in the context given.");
        }

        private void DisplayDiscountResults()
        {
            resultBLProducts = BLProduct.RetrieveDiscountedProductsFromDB(connectionString);
            ResultTitle.InnerText = "Sales";
            ResultImage.Src = "/Images/sales2016.jpg";
            ResultDescription.InnerText = " Dont miss out of this great sale of among our great products with up to 30 % discount. This sale only applies to the products in our current stock. Dont miss out!! First come, first served!";
            DisplayProducts();
        }

        private bool ValidateSearchString()
        {
            if (searchString.Contains(";"))
                return false;
            return true;
        }
        private void DisplaySearchResult()
        {
            string[] searchWords = searchString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string sqlSearchString = "%";
            for (int i = 0; i < searchWords.Length; i++)
                sqlSearchString += searchWords[i] + "%";
            resultBLProducts = BLProduct.RetrieveListFromDB(connectionString, $"Name LIKE '{sqlSearchString}'");
            if (resultBLProducts.Count > 0)
            {
                ResultTitle.InnerHtml = $"Search results for '{searchString}':";
                DisplayProducts();
            }
            else
                ResultTitle.InnerHtml = $"No results were found for '{searchString}'.";
        }
        
        private void DisplayCategoryResult()
        {
            BLCategory category = BLCategory.RetrieveFromDB(connectionString, categoryId);
            if(category == null)
                throw new HttpException(404, $"Result.aspx?categoryId={categoryId} doesn't exist.");
            resultBLProducts = BLProduct.RetrieveListFromDB(connectionString,
                $"CategoryID = {categoryId} AND ID NOT IN (SELECT VariantID FROM ProductVariants)");
            ResultTitle.InnerHtml = category.Name;
            ResultImage.Src = $"ImgHandler.ashx?categoryId={categoryId}";
            ResultDescription.InnerHtml = category.Description;
            DisplayProducts();
        }

        private void DisplayProducts()
        {
            int currentColumn = 0;
            const int LAST_COLUMN = 3;
            Panel[] tempPanelArray = new Panel[LAST_COLUMN + 1];

            foreach (BLProduct product in resultBLProducts)
            {
                Panel productPanel = new Panel { CssClass = "span3 result_product_container" };

                HyperLink productLink = new HyperLink { NavigateUrl = $"Product.aspx?productId={product.Id}" };
                Image productThumb = new Image
                {
                    CssClass = "image_thumbnail",
                    ImageUrl = $"ImgHandler.ashx?productIdThumb={product.Id}"
                };
                productLink.Controls.Add(productThumb);
                Label productNameLabel = new Label
                {
                    CssClass = "result_product_name",
                    Text = product.Name
                };
                productPanel.Controls.Add(productLink);
                productPanel.Controls.Add(productNameLabel);

                if (product.Discount > 0)
                {
                    Label productOriginalPriceLabel = new Label
                    {
                        CssClass = "result_product_original_price",
                        Text = "£" + product.Price
                    };
                    productPanel.Controls.Add(productOriginalPriceLabel);
                    double newPrice = product.Price - product.Price * product.Discount / 100;
                    Label productNewPriceLabel = new Label
                    {
                        CssClass = "result_product_discount_price",
                        Text = "£" + newPrice
                    };
                    productPanel.Controls.Add(productNewPriceLabel);
                }
                else
                {
                    Label productPriceLabel = new Label
                    {
                        CssClass = "result_product_price",
                        Text = "£" + product.Price
                    };
                    productPanel.Controls.Add(productPriceLabel);
                }

                Button productInfoButton = new Button
                {
                    CssClass = "result_product_infobutton",
                    Text = "More info",
                    PostBackUrl = $"Product.aspx?productId={product.Id}"
                };
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