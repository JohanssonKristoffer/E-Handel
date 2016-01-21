using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using E_Handel.BL;

namespace E_Handel
{

    public partial class Checkout : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["KKG-EHandelConnectionString"].ConnectionString;
        private List<BLCartProduct> cartList;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCartList();
            InsertProductsIntoTable();
        }

        private void InsertCustomerInformation()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO Orders (Postage,TotalPrice,Address,PostalCode,City,Country,Email,Telephone,Name,Surname)OUTPUT INSERTED.ID VALUES                     ('{tableShippingPrice}','{tableTotalPrice}','{customer_address.Text}','{customer_postalcode.Text}','{customer_city.Text}','{customer_country.Text}','{customer_email.Text}','{customer_phone.Text}','{customer_name.Text}','{customer_surname.Text}')", connection);
                connection.Open();

                Session["orderId"] = (int) cmd.ExecuteScalar();
            }
        }
        
        private void InsertProductsIntoTable()
        {
            foreach (var cartProduct in cartList)
            {
                BLProduct product = new BLProduct(connectionString, cartProduct.Id);

                Image productImage = new Image();
                productImage.ImageUrl = $"ImgHandler.ashx?productIdThumb={product.Id}";
                productImage.AlternateText = product.Name;
                productImage.CssClass = "productImage";
                TableCell cellImage = new TableCell();
                cellImage.CssClass = "td";
                cellImage.Controls.Add(productImage);
                
                Label nameLabel = new Label();
                nameLabel.Text = product.Name;
                TableCell cellName = new TableCell();
                cellName.CssClass = "td";
                cellName.Controls.Add(nameLabel);

                TextBox quantityTextBox = new TextBox();
                quantityTextBox.Text = cartProduct.Quantity.ToString(); 
                TableCell cellQuantity = new TableCell();
                cellQuantity.CssClass = "QuantityTextbox td";
                cellQuantity.Controls.Add(quantityTextBox);

                Label priceLabel = new Label();
                priceLabel.Text = product.Price.ToString();
                TableCell cellPrice = new TableCell();
                cellPrice.CssClass = "td";
                cellPrice.Controls.Add(priceLabel);

                Label stockLabel = new Label();
                stockLabel.Text = product.StockQuantity.ToString();
                TableCell cellStock = new TableCell();
                cellStock.CssClass = "td";
                cellStock.Controls.Add(stockLabel);

                Label totalPriceLabel = new Label();
                double totalPriceSum = cartProduct.Quantity * product.Price;
                totalPriceLabel.Text = totalPriceSum.ToString();
                TableCell celltotalPrice = new TableCell();
                celltotalPrice.CssClass = "td";
                celltotalPrice.Controls.Add(totalPriceLabel);

                Image updateCartImage = new Image();
                updateCartImage.ImageUrl = "/Images/updateCart.png";
                updateCartImage.AlternateText = "Update quantity";
                updateCartImage.ID = "updateCart";
                TableCell cellUpdateCart = new TableCell();
                cellUpdateCart.Controls.Add(updateCartImage);

                Image removeImage = new Image();
                removeImage.ImageUrl = "/Images/removeProduct.png";
                removeImage.AlternateText = "Delete this product";
                removeImage.ID = $"removeCart + {product.Id}";
                TableCell cellRemoveImage = new TableCell();
                cellRemoveImage.Controls.Add(removeImage);

                TableRow row = new TableRow();
                row.Controls.Add(cellImage);
                row.Controls.Add(cellName);
                row.Controls.Add(cellQuantity);
                row.Controls.Add(cellPrice);
                row.Controls.Add(cellStock);
                row.Controls.Add(celltotalPrice);
                row.Controls.Add(cellUpdateCart);
                row.Controls.Add(cellRemoveImage);

                Checkout_table.Controls.Add(row);

            }

        }

        private void LoadCartList()
        {
            if(Session["cartList"] != null)
                cartList = (List<BLCartProduct>)Session["cartList"];
            else
            {
                //error
            }
        }

        protected void customer_submit_order_Click(object sender, EventArgs e)
        {
            InsertCustomerInformation();
            InsertOrderProducts();
            Response.Redirect("Receipt.aspx");
        }

        private void InsertOrderProducts()
        {
            SqlConnection con = new SqlConnection(connectionString);

            int orderId = (int) Session["orderId"];
            try
            {
                con.Open();
                foreach (BLCartProduct product in cartList)
                {
                    SqlCommand insertProductInOrderProducts = new SqlCommand($"INSERT INTO OrderProducts (OrderID,ProductID,Quantity) VALUES ('{orderId}','{product.Id}', '{product.Quantity}')", con);
                    if (insertProductInOrderProducts.ExecuteNonQuery() < 1)
                    {
                        //error
                    }
                    insertProductInOrderProducts.Dispose();
                }

            }
            catch (Exception ex)
            {
                //error
            }
            finally
            {
                con.Close();
                con.Dispose();
            }




        }

        private void CalculateSum()
        {
            //BLCartProduct allBlCartProduct = new BLCartProduct();
            //int allCartProducts = allBlCartProduct.Quantity; 
            //tableTotalPrice.InnerText = $"";
        }

        private void ShippingDrowdown()
        {
        }
    }
}