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
            List<BLCartProduct> productList = new List<BLCartProduct>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["orderId"] = 2;
            LoadOrder();
            LoadProducts();
            ShowProducts();
        }

        private void ShowProducts()
        {
            foreach (var product in productList)
            {
                Label nameLabel = new Label();
                //nameLabel.Text = Product.LoadName(product.Id);
                ReceiptPanel.Controls.Add(nameLabel);
            }
        }

        private void LoadOrder()
        {
            SqlConnection con = new SqlConnection(connectionString);
            string sqlQueryLabels = $"SELECT * From Orders WHERE ID = {(int)Session["orderId"]}";

            SqlCommand cmdGetLabels = new SqlCommand(sqlQueryLabels, con);
            SqlDataReader oreader = null;
            try
            {
                con.Open();
                oreader = cmdGetLabels.ExecuteReader();
                while (oreader.Read())
                {
                    TotalPricelbl.InnerText = oreader["TotalPrice"].ToString();
                    VATlbl.InnerText = oreader["VAT"].ToString();
                    Postagelbl.InnerText = oreader["Postage"].ToString();
                    Addresslbl.InnerText = oreader["Address"].ToString();
                    PostalCodelbl.InnerText = oreader["PostalCode"].ToString();
                    Citylbl.InnerText = oreader["City"].ToString();
                    Countrylbl.InnerText = oreader["Country"].ToString();
                    Emaillbl.InnerText = oreader["Email"].ToString();
                    TelephoneNumberlbl.InnerText = oreader["Telephone"].ToString();
                    PaymentOptionslbl.InnerText = oreader["PaymentOptions"].ToString();
                    Deliveryoptionslbl.InnerText = oreader["DeliveryOptions"].ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (oreader != null)
                {
                    oreader.Close();
                    oreader.Dispose();
                }
                con.Close();
                con.Dispose();
                cmdGetLabels.Dispose();
            }
        }

        private void LoadProducts()
        {
            SqlConnection con = new SqlConnection(connectionString);
            string sqlQuery = $"SELECT * From OrderProducts WHERE OrderID = {(int)Session["orderId"]}";

            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataReader oreader = null;
            try
            {
                con.Open();
                oreader = cmd.ExecuteReader();
                while (oreader.Read())
                {
                    productList.Add(new BLCartProduct(int.Parse(oreader["ProductID"].ToString()), int.Parse(oreader["Quantity"].ToString())));
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (oreader != null)
                {
                    oreader.Close();
                    oreader.Dispose();
                }
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
        }
    }
}