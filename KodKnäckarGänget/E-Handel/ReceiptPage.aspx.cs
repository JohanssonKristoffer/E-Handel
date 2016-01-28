using System;
using System.Web.UI.WebControls;
using System.Configuration;
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
            address.InnerText = order.Address;
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
            double vatTotal = 0;
            double sumTotal = 0;
            foreach (var cartProduct in order.CartProducts)
            {
                BLProduct product = BLProduct.RetrieveFromDB(connectionString, cartProduct.Id);

                TableCell titleCell = new TableCell { Text = product.Name };
                TableCell priceCell = new TableCell { Text = "£" + product.Price * (1 - product.Discount / 100)};
                TableCell amountCell = new TableCell { Text = cartProduct.Quantity.ToString() };
                double sum = cartProduct.Quantity*product.Price*(1 - product.Discount/100);
                sumTotal += sum;
                double vat = (product.VAT*sum/(100 + product.VAT));
                vatTotal += vat;
                TableCell vatCell = new TableCell { Text = "£" + vat };
                TableCell sumCell = new TableCell { Text = "£" + sum };

                TableRow row = new TableRow();
                row.Controls.Add(titleCell);
                row.Controls.Add(priceCell);
                row.Controls.Add(amountCell);
                row.Controls.Add(vatCell);
                row.Controls.Add(sumCell);

                productTable.Controls.Add(row);
            }
            TableCell titleFoot = new TableCell();
            TableCell priceFoot = new TableCell();
            TableCell amountFoot = new TableCell();
            TableCell vatFoot = new TableCell { Text = "£" + vatTotal };
            TableCell sumFoot = new TableCell { Text = "£" + sumTotal };

            TableRow footRow = new TableRow();
            footRow.Controls.Add(titleFoot);
            footRow.Controls.Add(priceFoot);
            footRow.Controls.Add(amountFoot);
            footRow.Controls.Add(vatFoot);
            footRow.Controls.Add(sumFoot);

            productTable.Controls.Add(footRow);
        }

        protected void receiptHomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.ResolveClientUrl("/Home.aspx"));
        }

        protected void receiptPrintButton_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "key", "window.print()", true);
        }
    }
}