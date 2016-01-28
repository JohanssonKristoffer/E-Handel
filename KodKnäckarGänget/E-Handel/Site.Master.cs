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
    public partial class Site : System.Web.UI.MasterPage
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["KKG-EHandelConnectionString"].ConnectionString;
        private List<BLCartProduct> cartList;

        protected void Page_Load(object sender, EventArgs e)
        {
            MoveFooter();
            PopulateCategoryDropdown();
            HideCartOnCheckout();
            RetrieveCartCount();
            if (TryRetrieveCartList())
            {
                GenerateCartContent();
                CartDropDownTableText.Visible = false;
            }
            else
            {
                CartDropDownTableHeader.Visible = false;
                CheckoutButton.Visible = false;
            }
        }

        private void GenerateCartContent()
        {
            try
            {
                foreach (var cartProduct in cartList)
                {
                    BLProduct product = BLProduct.RetrieveFromDB(connectionString, cartProduct.Id);

                    HyperLink productLink = new HyperLink {NavigateUrl = $"Product.aspx?productId={product.Id}"};
                    Image productImage = new Image
                    {
                        ImageUrl = $"ImgHandler.ashx?productIdThumb={product.Id}",
                        AlternateText = product.Name,
                        CssClass = "productImage"
                    };
                    productLink.Controls.Add(productImage);
                    TableCell cellImage = new TableCell {CssClass = "td"};
                    cellImage.Controls.Add(productLink);

                    Label nameLabel = new Label {Text = product.Name};
                    TableCell cellName = new TableCell {CssClass = "td"};
                    cellName.Controls.Add(nameLabel);

                    Label priceLabel = new Label {Text = "£" + (product.Price*(1 - product.Discount/100))};
                    TableCell cellPrice = new TableCell {CssClass = "td"};
                    cellPrice.Controls.Add(priceLabel);

                    Label quantityLabel = new Label
                    {
                        Text = cartProduct.Quantity.ToString(),
                        ID = $"quantity{product.Id}",
                        Enabled = false
                    };
                    TableCell cellQuantity = new TableCell {CssClass = "td"};
                    cellQuantity.Controls.Add(quantityLabel);

                    double totalPriceSum = cartProduct.Quantity*product.Price*(1 - product.Discount/100);
                    Label totalPriceLabel = new Label {Text = "£" + totalPriceSum};
                    TableCell celltotalPrice = new TableCell {CssClass = "td"};
                    celltotalPrice.Controls.Add(totalPriceLabel);

                    TableRow row = new TableRow();
                    row.Controls.Add(cellImage);
                    row.Controls.Add(cellName);
                    row.Controls.Add(cellPrice);
                    row.Controls.Add(cellQuantity);
                    row.Controls.Add(celltotalPrice);

                    CartListTable.Controls.Add(row);
                }
            }
            catch (Exception)
            {
                Session["cartList"] = null;
                Session["cartCount"] = null;
                throw; //Error retrieving product from Products
            }
        }
        private void PopulateCategoryDropdown()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlGetCategories = new SqlCommand("SELECT ID, Name FROM Categories", sqlConnection);
            SqlDataReader sqlReader = null;
            try
            {
                sqlConnection.Open();
                sqlReader = sqlGetCategories.ExecuteReader();
                while (sqlReader.Read())
                {
                    Label listItem = new Label();

                    listItem.Text = $"<li><a href='/Result.aspx?categoryId={sqlReader["ID"]}'>{sqlReader["Name"]}</a></li>";
                    categoryDropdownMenu.Controls.Add(listItem);
                }
            }
            catch (Exception)
            {
                throw; //Error retrieving categories from Categories
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

        private bool TryRetrieveCartList()
        {
            if (Session["cartList"] != null)
            {
                cartList = (List<BLCartProduct>)Session["cartList"];
                return true;
            }
            return false;
        }

        private void HideCartOnCheckout()
        {
            if (Request.Url.ToString().Contains("/Checkout.aspx"))
                CartLi.Visible = false;
        }

        private void MoveFooter()
        {
            if (Request.Url.ToString().Contains("/ErrorDefault.aspx") ||
                Request.Url.ToString().Contains("/ErrorDefault.aspx"))
            {
                MainFooter.Style["bottom"] = "0px";
            }
       }

        private void RetrieveCartCount()
        {
            if (Session["cartCount"] != null)
            {
                CartCountLabel.Text = ((int) Session["cartCount"]).ToString();
            }
            else
            {
                CartCountLabel.Visible = false;
            }
        }

        protected void SendSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/Result.aspx?search={SearchBox.Text}");
        }
    }
}