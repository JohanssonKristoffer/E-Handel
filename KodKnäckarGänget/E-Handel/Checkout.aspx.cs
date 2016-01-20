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

namespace E_Handel
{

    public partial class Checkout : System.Web.UI.Page
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["KKG-EHandelConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void customer_submit_order_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = $"INSERT INTO Orders (Postage,TotalPrice,Address,PostalCode,City,Country,Email,Telephone,Name,Surname) VALUES                     ({customer_address.Text}','{customer_postalcode.Text}','{customer_city.Text}','{customer_country.Text}','{customer_email.Text}','{customer_phone.Text}','{customer_name.Text}','{customer_surname.Text}'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
            Response.Redirect ("reciept.aspx");
        }
    }
}

// Post
// DHL

    // Cash
    // Card
    // Paypal