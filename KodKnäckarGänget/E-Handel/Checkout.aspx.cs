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
        private double totalPrice = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (TryLoadCartList())
            {
                //GenerateDummyData();
                CreateTableCellsFromCartList();
            }
            else
            {
                // error
                Response.Redirect("Home.aspx");
            }
        }

        private bool TryLoadCartList()
        {
            if (Session["cartList"] != null)
            {
                cartList = (List<BLCartProduct>)Session["cartList"];
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GenerateDummyData()
        {
            List<BLCartProduct> cartList = new List<BLCartProduct>();
            BLCartProduct cartProductOne = new BLCartProduct(10, 5);
            BLCartProduct cartProductTwo = new BLCartProduct(11, 2);
            BLCartProduct cartProductThree = new BLCartProduct(41, 5);

            cartList.Add(cartProductOne);
            cartList.Add(cartProductTwo);
            cartList.Add(cartProductThree);

            Session["cartList"] = cartList;
        }

        private string ShippingDrowdown(out double shippingCost)
        {
            switch (shipping_dropdown.SelectedValue)
            {
                case "1":
                    shippingCost = 0;
                    return "Pickup at store";
                case "2":
                    shippingCost = 2;
                    return "Standard Mail";

                default:
                    shippingCost = 8;
                    return "DHL";
            }
        }

        private string PaymentDropdown()
        {
            switch (payment_dropdown.SelectedValue)
            {
                case "1":
                    return "Cash at pickup";
                case "2":
                    return "Invoice";
                default:
                    return "Debit card";
            }
        }

        private void CreateTableCellsFromCartList()
        {
            foreach (var cartProduct in cartList)
            {
                BLProduct product = new BLProduct(connectionString, cartProduct.Id);

                Image productImage = new Image();
                productImage.ImageUrl = $"ImgHandler.ashx?productIdThumb={product.Id}";
                productImage.AlternateText = product.Name;
                productImage.CssClass = "productImage";
                TableCell cellImage = new TableCell();
                cellImage.Controls.Add(productImage);

                Label nameLabel = new Label();
                nameLabel.Text = product.Name;
                TableCell cellName = new TableCell();
                cellName.Controls.Add(nameLabel);

                Panel quantityPanel = new Panel();
                Label quantityLabel = new Label() { Enabled = false };
                quantityLabel.Text = cartProduct.Quantity.ToString();
                TableCell cellQuantity = new TableCell();
                cellQuantity.CssClass = "QuantityTextbox td";

                Button increaseEditCartButton = new Button()
                {
                    Text = "+",
                    ID = $"increaseEditCartButton_{product.Id}"
                };
                increaseEditCartButton.Click += IncreaseEditCartButton_Click;

                Button decreaseEditCartButton = new Button()
                {
                    Text = "-",
                    ID = $"decreaseEditCartButton{product.Id}"
                };
                decreaseEditCartButton.Click += DecreaseEditCartButton_Click;

                quantityPanel.Controls.Add(decreaseEditCartButton);
                quantityPanel.Controls.Add(quantityLabel);
                quantityPanel.Controls.Add(increaseEditCartButton);
                cellQuantity.Controls.Add(quantityPanel);

                Label priceLabel = new Label();
                priceLabel.Text = product.Price.ToString();
                TableCell cellPrice = new TableCell();
                cellPrice.Controls.Add(priceLabel);

                Label totalPriceLabel = new Label();
                double totalPriceSum = cartProduct.Quantity * product.Price;
                totalPriceLabel.Text = totalPriceSum.ToString();
                TableCell celltotalPrice = new TableCell();
                celltotalPrice.Controls.Add(totalPriceLabel);

                Label stockLabel = new Label();
                stockLabel.Text = product.StockQuantity.ToString();
                TableCell cellStock = new TableCell();
                cellStock.Controls.Add(stockLabel);

                Button editCartButton = new Button();
                editCartButton.Text = "Edit quantity";
                TableCell CellEditCartButton = new TableCell();
                CellEditCartButton.Controls.Add(editCartButton);

                Button removeProductButton = new Button()
                {
                    Text = "Remove from cart",
                    ID = $"removeButton_{product.Id}"
                };
                removeProductButton.Click += RemoveButton_Click;
                TableCell CellRemoveProductButton = new TableCell();
                CellRemoveProductButton.Controls.Add(removeProductButton);

                TableRow row = new TableRow();
                row.Controls.Add(cellImage);
                row.Controls.Add(cellName);
                row.Controls.Add(cellPrice);
                row.Controls.Add(cellQuantity);
                row.Controls.Add(cellStock);
                row.Controls.Add(celltotalPrice);
                row.Controls.Add(CellEditCartButton);
                row.Controls.Add(CellRemoveProductButton);

                checkout_product_table.Controls.Add(row);
            }
        }

        private void DecreaseEditCartButton_Click(object sender, EventArgs e)
        {
            Button buttonClickedDecrease = (Button)sender;
            int productId = int.Parse(buttonClickedDecrease.ID.Split('_')[1]);
            //BLCartProduct newProduct = new BLCartProduct(productId);


            foreach (BLCartProduct cartProduct in cartList)
            {
                if (cartProduct.Quantity >= 1)
                {
                    int cartCount = (int)Session["cartCount"];
                    cartCount -= cartProduct.Quantity;
                    Session["cartCount"] = cartCount;
                    cartProduct.Quantity--;
                    break;
                }
            }
            if ((int)Session["cartCount"] == 0)
            {
                Session["cartList"] = null;
                Session["cartCount"] = null;
                Response.Redirect("Home.aspx");
            }

            Session["cartList"] = cartList;
            Response.Redirect("Checkout.aspx");
        }

        private void IncreaseEditCartButton_Click(object sender, EventArgs e)
        {
            Button buttonClickedIncrease = (Button)sender;
            int productId = int.Parse(buttonClickedIncrease.ID.Split('_')[1]);

            foreach (BLCartProduct cartProduct in cartList)
            {
                if (cartProduct.Quantity >= 1)
                {
                    int cartCount = (int)Session["cartCount"];
                    cartCount += cartProduct.Quantity;
                    Session["cartCount"] = cartCount;
                    cartProduct.Quantity++;
                    break;
                }
            }
            if ((int)Session["cartCount"] == 0)
            {
                Session["cartList"] = null;
                Session["cartCount"] = null;
                Response.Redirect("Home.aspx");
            }
            Session["cartList"] = cartList;
            Response.Redirect("Checkout.aspx");
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            Button buttonClicked = (Button)sender;
            int productId = int.Parse(buttonClicked.ID.Split('_')[1]);

            foreach (BLCartProduct cartProduct in cartList)
            {
                if (cartProduct.Id == productId)
                {
                    int cartCount = (int)Session["cartCount"];
                    cartCount -= cartProduct.Quantity;
                    Session["cartCount"] = cartCount;
                    cartList.Remove(cartProduct);
                    break;
                }
            }
            if ((int)Session["cartCount"] == 0)
            {
                Session["cartList"] = null;
                Session["cartCount"] = null;
                Response.Redirect("Home.aspx");
            }
            Session["cartList"] = cartList;
            Response.Redirect("Checkout.aspx");
        }

        protected void SubmitOrder_Click(object sender, EventArgs e)
        {
            InsertCustomerInformation();
            InsertOrderProducts();
            Response.Redirect("ReceiptPage.aspx");
        }

        private void InsertCustomerInformation()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO Orders (Postage,TotalPrice,Address,PostalCode,City,Country,Email,Telephone,PaymentOptions,DeliveryOptions,Name,Surname)OUTPUT INSERTED.ID VALUES (@postage, @totalPrice, @address, @postalCode, @city, @country, @email,@telephone, @paymentoptions, @deliveryoptions, @name, @surname)", connection);
                connection.Open();

                double shippingCost;
                string shippingMethod = ShippingDrowdown(out shippingCost);

                cmd.Parameters.Add("@postage", SqlDbType.Float).Value = shippingCost;
                cmd.Parameters.Add("@totalPrice", SqlDbType.Float).Value = totalPrice;
                cmd.Parameters.Add("@address", SqlDbType.NVarChar, 255).Value = customer_address.Text;
                cmd.Parameters.Add("@postalCode", SqlDbType.NVarChar, 50).Value = customer_postalcode.Text;
                cmd.Parameters.Add("@city", SqlDbType.NVarChar, 50).Value = customer_city.Text;
                cmd.Parameters.Add("@country", SqlDbType.NVarChar, 50).Value = customer_country.Text;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = customer_email.Text;
                cmd.Parameters.Add("@telephone", SqlDbType.NVarChar, 50).Value = customer_phone.Text;
                cmd.Parameters.Add("@paymentoptions", SqlDbType.NVarChar, 50).Value = PaymentDropdown();
                cmd.Parameters.Add("@deliveryoptions", SqlDbType.NVarChar, 50).Value = shippingMethod;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = customer_name.Text;
                cmd.Parameters.Add("@surname", SqlDbType.NVarChar, 50).Value = customer_surname.Text;

                Session["orderId"] = (int)cmd.ExecuteScalar();
            }
        }

        private void InsertOrderProducts()
        {
            SqlConnection con = new SqlConnection(connectionString);

            int orderId = (int)Session["orderId"];
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
    }
}