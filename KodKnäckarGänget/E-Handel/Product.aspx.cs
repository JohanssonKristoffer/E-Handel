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
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["KKG-EHandelConnectionString"].ConnectionString;
        int productId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["productId"], out productId))
            {
                productImage.Src = $"ImgHandler.ashx?productId={productId}";
                LoadName();
                LoadDescription();
                LoadPrice();
                LoadVariant();
            }
            else
            {
                productTitle.InnerText = "Unable to retrieve product.";//error
            }
        }

        public void LoadName()
        {
            SqlConnection con = new SqlConnection(connectionString);

            string sqlQueryName = $"SELECT Name FROM Products WHERE ID = {productId}";

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
                productTitle.InnerText = ex.Message; //error
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

            string sqlQueryPrice = $"SELECT Price FROM Products WHERE ID = {productId}";

            SqlCommand cmd = new SqlCommand(sqlQueryPrice, con);
            SqlDataReader oreader = null;
            try
            {
                con.Open();
                oreader = cmd.ExecuteReader();
                while (oreader.Read())
                {
                    productPrice.InnerText = "£" + oreader["Price"].ToString();
                }
            }
            catch (Exception ex)
            {
                productPrice.InnerText = ex.Message; //error
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

        public void LoadDescription()
        {
            SqlConnection con = new SqlConnection(connectionString);

            string sqlQueryDescription = $"SELECT Description FROM Products WHERE ID = {productId}";
            SqlCommand cmd = new SqlCommand(sqlQueryDescription, con);
            SqlDataReader oreader = null;
            try
            {
                con.Open();
                oreader = cmd.ExecuteReader();
                while (oreader.Read())
                {
                    productDescription.InnerText = oreader["Description"].ToString();
                }
            }
            catch (Exception ex)
            {
                productDescription.InnerText = ex.Message;
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

        private void LoadVariant()
        {
            //SqlConnection con = new SqlConnection(connectionString);

            //string sqlQueryDescription = $"SELECT Description FROM Products WHERE ID = {productId}";
            //SqlCommand cmd = new SqlCommand(sqlQueryDescription, con);
            //SqlDataReader oreader = null;
            //try
            //{
            //    con.Open();
            //    oreader = cmd.ExecuteReader();
            //    DropDownVariants.Items.Clear();
            //    ListItem standardItem = new ListItem { Text = productTitle.InnerText, Value = productId.ToString() };
            //    DropDownVariants.Items.Add(standardItem);
            //    while (oreader.Read())
            //    {
            //        ListItem item = new ListItem(oreader["Name"].ToString(), oreader["ProductCategoryID"].ToString());
            //        DropDownVariants.Items.Add(item);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    productDescription.InnerText = ex.Message;
            //}
            //finally
            //{
            //    if (oreader != null)
            //    {
            //        oreader.Close();
            //        oreader.Dispose();
            //    }
            //    con.Close();
            //    con.Dispose();
            //    cmd.Dispose();
            //}
        }
    }
}