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
                Label nameAndQuantityLabel = new Label
                {
                    Text = "<li>" + product.Name + " Amount: " + cartProduct.Quantity + "</li>"
                };
                ulReceipt.InnerHtml += nameAndQuantityLabel.Text;
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