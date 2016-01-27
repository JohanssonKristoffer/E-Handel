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
            try
            {
                order = BLOrder.RetrieveFromDB(connectionString, (int)Session["orderId"]);
                ShowOrder();
            }
            catch (Exception)
            {
                throw; //Error retrieving order from Orders or OrderProducts
            }
        }

        private void ShowOrder()
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
            try
            {
                foreach (var cartProduct in order.CartProducts)
                {
                    BLProduct product = BLProduct.RetrieveFromDB(connectionString, cartProduct.Id);
                    Label nameAndQuantityLabel = new Label();
                    nameAndQuantityLabel.Text = product.Name + "Amount: " + cartProduct.Quantity;
                    ReceiptPanel.Controls.Add(nameAndQuantityLabel);
                }
            }
            catch (Exception)
            {
                throw; //Error retrieving product from Products
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