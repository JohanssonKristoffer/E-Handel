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

                cmd.CommandText = "INSERT INTO Orders (TotalPrice,Postage,Address,PostalCode,City,Country,Email,Telephone,Name,Surname) VALUES (" + customer_phone.Text + "," + customer_address.Text + "," + customer_postalcode.Text + "," + customer_city.Text + "," + customer_country.Text + "," + customer_email.Text + " + customer_phone.Text + "," + customer_name.Text + "," + customer_surname.Text + ");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

/*<asp:TextBox ID="customer_name" runat="server"></asp:TextBox>
            <asp:TextBox ID="customer_surname" runat="server"></asp:TextBox>

            <asp:TextBox ID="customer_email" runat="server"></asp:TextBox>
            <asp:TextBox ID="customer_phone" runat="server"></asp:TextBox>
            <asp:TextBox ID="customer_address" runat="server"></asp:TextBox>
            <asp:TextBox ID="customer_postalcode" runat="server"></asp:TextBox>
            <asp:TextBox ID="customer_city" runat="server"></asp:TextBox>
            <asp:TextBox ID="customer_country" runat="server"></asp:TextBox>*/
