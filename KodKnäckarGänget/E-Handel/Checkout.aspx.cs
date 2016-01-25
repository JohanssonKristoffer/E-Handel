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
        private double totalCartPrice;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (TryLoadCartList())
                CreateTableCellsFromCartList();
            else
            {
                //Error attempting to retrieve empty cartlist
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
            return false;
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
            try
            {
                foreach (var cartProduct in cartList)
                {
                    BLProduct product = new BLProduct(connectionString, cartProduct.Id);

                    HyperLink linkProduct = new HyperLink {NavigateUrl = $"Product.aspx?productId={product.Id}"};
                    Image productImage = new Image
                    {
                        ImageUrl = $"ImgHandler.ashx?productIdThumb={product.Id}",
                        AlternateText = product.Name,
                        CssClass = "productImage"
                    };
                    linkProduct.Controls.Add(productImage);
                    TableCell cellImage = new TableCell();
                    cellImage.Controls.Add(linkProduct);

                    Label nameLabel = new Label {Text = product.Name};
                    TableCell cellName = new TableCell();
                    cellName.Controls.Add(nameLabel);

                    Label priceLabel = new Label {Text = "£" + (product.Price*(1 - product.Discount/100))};
                    TableCell cellPrice = new TableCell();
                    cellPrice.Controls.Add(priceLabel);

                    Panel quantityPanel = new Panel();
                    Label quantityLabel = new Label
                    {
                        Enabled = false,
                        Text = cartProduct.Quantity.ToString()
                    };
                    Button increaseEditCartButton = new Button()
                    {
                        Text = "+",
                        ID = $"increaseEditCartButton_{product.Id}"
                    };
                    increaseEditCartButton.Click += IncreaseEditCartButton_Click;
                    if (cartProduct.Quantity >= product.StockQuantity)
                        increaseEditCartButton.Enabled = false;
                    Button decreaseEditCartButton = new Button()
                    {
                        Text = "-",
                        ID = $"decreaseEditCartButton_{product.Id}"
                    };
                    decreaseEditCartButton.Click += DecreaseEditCartButton_Click;
                    if (cartProduct.Quantity <= 1)
                        decreaseEditCartButton.Enabled = false;

                    TableCell cellQuantity = new TableCell {CssClass = "QuantityTextbox td"};
                    quantityPanel.Controls.Add(decreaseEditCartButton);
                    quantityPanel.Controls.Add(quantityLabel);
                    quantityPanel.Controls.Add(increaseEditCartButton);
                    cellQuantity.Controls.Add(quantityPanel);

                    Label stockLabel = new Label {Text = product.StockQuantity.ToString()};
                    TableCell cellStock = new TableCell();
                    cellStock.Controls.Add(stockLabel);

                    Label vatLabel = new Label {Text = product.VAT + "%"};
                    TableCell cellVAT = new TableCell();
                    cellVAT.Controls.Add(vatLabel);

                    double totalPriceSum = cartProduct.Quantity*product.Price*(1 - product.Discount/100);
                    totalCartPrice += totalPriceSum;
                    Label totalPriceLabel = new Label {Text = "£" + totalPriceSum};
                    TableCell celltotalPrice = new TableCell();
                    celltotalPrice.Controls.Add(totalPriceLabel);

                    Button removeProductButton = new Button()
                    {
                        Text = "Remove from cart",
                        ID = $"removeButton_{product.Id}"
                    };
                    removeProductButton.Click += RemoveButton_Click;
                    TableCell cellRemoveProductButton = new TableCell();
                    cellRemoveProductButton.Controls.Add(removeProductButton);

                    TableRow row = new TableRow();
                    row.Controls.Add(cellImage);
                    row.Controls.Add(cellName);
                    row.Controls.Add(cellPrice);
                    row.Controls.Add(cellQuantity);
                    row.Controls.Add(cellStock);
                    row.Controls.Add(cellVAT);
                    row.Controls.Add(celltotalPrice);
                    row.Controls.Add(cellRemoveProductButton);

                    checkout_product_table.Controls.Add(row);
                }
                double shippingCost;
                ShippingDrowdown(out shippingCost);
                tableShippingPrice.InnerText = "£" + shippingCost;
                totalCartPrice += shippingCost;
                tableTotalPrice.InnerText = "£" + totalCartPrice;
            }
            catch (Exception)
            {
                throw; //Error retrieving product from Products
            }
        }

        private void DecreaseEditCartButton_Click(object sender, EventArgs e)
        {
            int productId = int.Parse(((Button)sender).ID.Split('_')[1]);

            foreach (BLCartProduct cartProduct in cartList)
            {
                if (cartProduct.Id == productId)
                {
                    int cartCount = (int)Session["cartCount"];
                    cartCount--;
                    Session["cartCount"] = cartCount;
                    cartProduct.Quantity--;
                    break;
                }
            }

            Session["cartList"] = cartList;
            Response.Redirect("Checkout.aspx");
        }

        private void IncreaseEditCartButton_Click(object sender, EventArgs e)
        {
            int productId = int.Parse(((Button)sender).ID.Split('_')[1]);

            foreach (BLCartProduct cartProduct in cartList)
            {
                if (cartProduct.Id == productId)
                {
                    int cartCount = (int)Session["cartCount"];
                    cartCount++;
                    Session["cartCount"] = cartCount;
                    cartProduct.Quantity++;
                    break;
                }
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
            if (Page.IsValid && cartList != null)
            {
                try
                {
                    double shippingCost;
                    string shippingMethod = ShippingDrowdown(out shippingCost);
                    BLOrder order = new BLOrder(postage: shippingCost, totalPrice: totalCartPrice, address: customer_address.Text, postalCode: customer_postalcode.Text, city: customer_city.Text, country: customer_country.Text, email: customer_email.Text, telephone: customer_phone.Text, paymentOptions: PaymentDropdown(), deliveryOptions: shippingMethod, name: customer_name.Text, surname: customer_surname.Text, cartProducts: cartList);
                    order.InsertIntoDB(connectionString);
                    Session["orderId"] = order.Id;
                    Response.Redirect("ReceiptPage.aspx");
                }
                catch (Exception)
                {
                    throw; //Error inserting order into database
                }
            }
        }
    }
}