using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using E_Handel.BL;


namespace E_Handel
{
    public partial class ReceiptPage : System.Web.UI.Page
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["KKG-EHandelConnectionString"].ConnectionString;
        private BLOrder order;

        protected void Page_Load(object sender, EventArgs e)
        {
            order = BLOrder.RetrieveFromDB(connectionString, (int)Session["orderId"]);
            ShowOrderInfo();
            ShowOrderProducts();
        }

        private void ShowOrderInfo()
        {
            orderIdOutput.InnerText = order.Id.ToString();
            totalPrice.InnerText = "£" + order.TotalPrice;
            postage.InnerText = "£" + order.Postage;
            adress.InnerText = order.Address;
            postalCode.InnerText = order.PostalCode;
            city.InnerText = order.City;
            country.InnerText = order.Country;
            email.InnerText = order.Email;
            phoneNumber.InnerText = order.Telephone;
            paymentOptions.InnerText = order.PaymentOptions;
            deliveryOptions.InnerText = order.DeliveryOptions;
        }

        private void ShowOrderProducts()
        {
            foreach (var cartProduct in order.CartProducts)
            {
                BLProduct product = BLProduct.RetrieveFromDB(connectionString, cartProduct.Id);

                TableCell title = new TableCell { Text = product.Name };
                TableCell amount = new TableCell { Text = cartProduct.Quantity.ToString() };
                TableCell price = new TableCell { Text = "£" + product.Price.ToString() };
                TableCell totalPrice = new TableCell { Text = "£" + (cartProduct.Quantity * product.Price).ToString() };
                TableCell vat = new TableCell { Text = "£" + (cartProduct.Quantity * product.Price * 0.2).ToString() };
                TableRow row = new TableRow();

                row.Controls.Add(title);
                row.Controls.Add(amount);
                row.Controls.Add(price);
                row.Controls.Add(vat);
                row.Controls.Add(totalPrice);

                productTable.Controls.Add(row);
            }
        }

        protected void receiptHomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.ResolveClientUrl("/Home.aspx"));
        }

        protected void receiptPrintButton_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "key", "window.print()", true);
        }
    }
}