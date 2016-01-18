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

    public partial class Product : System.Web.UI.Page
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["KKG-EHandelConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            productImage.Src = $"ImgHandler.ashx?productId={GetProductId()}";
            LoadName();
            LoadDescription();
            LoadPrice();
        }

        public int GetProductId()
        {
            int productId;
            int.TryParse(Request.QueryString["productId"], out productId);
            return productId;
        }

        public void LoadName()
        {
            SqlConnection con = new SqlConnection(connectionString);
            // todo kolla connection string och validera querystring error page 

            string sqlQueryName = $"SELECT Name FROM Products WHERE ID = {GetProductId()}";

            SqlCommand cmd = new SqlCommand(sqlQueryName, con);
            SqlDataReader oreader = null;
            try
            {
                con.Open();
                oreader = cmd.ExecuteReader();
                while (oreader.Read())
                {
                    productTitle.InnerText = oreader["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                productTitle.InnerText = ex.Message;
                productTitle.Visible = true;
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

        public void LoadPrice()
        {
            SqlConnection con = new SqlConnection(connectionString);

            string sqlQueryPrice = $"SELECT Price FROM Products WHERE ID = {GetProductId()}";

            SqlCommand cmd = new SqlCommand(sqlQueryPrice, con);
            SqlDataReader oreader;
            try
            {
                con.Open();
                oreader = cmd.ExecuteReader();
                while (oreader.Read())
                {
                    productPrice.InnerText = "£" + oreader["Price"].ToString();
                }
                oreader.Close();
                oreader.Dispose();
            }
            catch (Exception ex)
            {
                productPrice.InnerText = ex.Message;
                productPrice.Visible = true;
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
        }

        public void LoadDescription()
        {
            SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=KKG-EHandel;User ID=Akwasi; Password=Root;Integrated Security = True");

            string sqlQueryDescription = $"SELECT Description FROM Products WHERE ID = {GetProductId()}";
            SqlCommand cmd = new SqlCommand(sqlQueryDescription, con);
            SqlDataReader oreader;
            try
            {
                con.Open();
                oreader = cmd.ExecuteReader();
                while (oreader.Read())
                {
                    productDescription.InnerText = oreader["Description"].ToString();
                }
                oreader.Close();
                oreader.Dispose();
            }
            catch (Exception ex)
            {
                productDescription.InnerText = ex.Message;
                productDescription.Visible = true;
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }

        }

    }
}