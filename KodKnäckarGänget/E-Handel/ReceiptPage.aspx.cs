using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;


namespace E_Handel
{
    public partial class ReceiptPage : System.Web.UI.Page
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["KKG-EHandelConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["orderId"] = 2;
            LoadLabels();
        }

        private void LoadLabels()
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
        }
    }
}