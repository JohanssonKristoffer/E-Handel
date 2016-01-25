using System;
using System.Collections.Generic;
using System.Configuration;
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
            foreach (var cartProduct in cartList)
            {
                BLProduct product = new BLProduct(connectionString, cartProduct.Id);

                HyperLink productLink = new HyperLink { NavigateUrl = $"Product.aspx?productId={product.Id}" };
                Image productImage = new Image
                {
                    ImageUrl = $"ImgHandler.ashx?productIdThumb={product.Id}",
                    AlternateText = product.Name,
                    CssClass = "productImage"
                };
                productLink.Controls.Add(productImage);
                TableCell cellImage = new TableCell { CssClass = "td" };
                cellImage.Controls.Add(productLink);

                Label nameLabel = new Label { Text = product.Name };
                TableCell cellName = new TableCell { CssClass = "td" };
                cellName.Controls.Add(nameLabel);

                Label priceLabel = new Label { Text = "£" + (product.Price * (1 - product.Discount / 100)) };
                TableCell cellPrice = new TableCell { CssClass = "td" };
                cellPrice.Controls.Add(priceLabel);

                Label quantityLabel = new Label { Text = cartProduct.Quantity.ToString(), ID = $"quantity{product.Id}", Enabled = false };
                TableCell cellQuantity = new TableCell { CssClass = "td" };
                cellQuantity.Controls.Add(quantityLabel);

                double totalPriceSum = cartProduct.Quantity * product.Price * (1 - product.Discount / 100);
                Label totalPriceLabel = new Label { Text = "£" + totalPriceSum };
                TableCell celltotalPrice = new TableCell { CssClass = "td" };
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

        private bool TryRetrieveCartList()
        {
            if (Session["cartList"] != null)
            {
                cartList = (List<BLCartProduct>)Session["cartList"];
                return true;
            }
            else
                return false;
        }

        private void HideCartOnCheckout()
        {
            if (Request.Url.ToString().Contains("Checkout.aspx"))
                CartLi.Visible = false;
        }

        private void RetrieveCartCount()
        {
            if (Session["cartCount"] != null)
                CartCountLabel.Text = ((int)Session["cartCount"]).ToString();
        }

        protected void SendSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect($"Result.aspx?search={SearchBox.Text}");
        }

        protected void SendCategoryChoice_SelectChange(object sender, EventArgs e)
        {
            if (DropDownCategories.SelectedValue != "0")
                Response.Redirect($"Result.aspx?categoryId={DropDownCategories.SelectedValue}");
        }
    }
}