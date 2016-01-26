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
                order = new BLOrder(connectionString, (int)Session["orderId"]);
                ShowOrder();
            }
            catch (Exception)
            {
                throw; //Error retrieving order from Orders or OrderProducts
            }
        }

        private void ShowOrder()
        {
            OrderIdlbl.InnerText = order.Id.ToString();
            TotalPricelbl.InnerText = "£" + order.TotalPrice;
            Postagelbl.InnerText = "£" + order.Postage;
            Addresslbl.InnerText = order.Address;
            PostalCodelbl.InnerText = order.PostalCode;
            Citylbl.InnerText = order.City;
            Countrylbl.InnerText = order.Country;
            Emaillbl.InnerText = order.Email;
            TelephoneNumberlbl.InnerText = order.Telephone;
            PaymentOptionslbl.InnerText = order.PaymentOptions;
            Deliveryoptionslbl.InnerText = order.DeliveryOptions;
            try
            {
                foreach (var cartProduct in order.CartProducts)
                {
                    BLProduct product = new BLProduct(connectionString, cartProduct.Id);
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
            Response.Redirect(Page.ResolveClientUrl("Home.aspx/"));
        }

        protected void receiptPrintButton_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "key", "window.print()", true);
        }
    }
}