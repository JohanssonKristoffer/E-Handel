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
        List<int> variantIdList = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["productId"], out productId))
            {
                productImage.Src = $"ImgHandler.ashx?productId={productId}";
                productTitle.InnerText=LoadName(productId);
                LoadDescription();
                LoadPrice();
                LoadVariant();
                ShowVariants();
            }
            else
            {
                productTitle.InnerText = "Unable to retrieve product.";//error
            }
        }


        public string LoadName(int id)
        {
            SqlConnection con = new SqlConnection(connectionString);

            string sqlQueryName = $"SELECT Name FROM Products WHERE ID = {id}";

            SqlCommand cmd = new SqlCommand(sqlQueryName, con);
            SqlDataReader oreader = null;
            try
            {
                con.Open();
                oreader = cmd.ExecuteReader();
                while (oreader.Read())
                {
                     return oreader["Name"].ToString();
                }
                return "Name not found.";
            }
            catch (Exception ex)
            { 
                return "ex.Message"; //error
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
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataReader oreader = null;
            try
            {
                con.Open();

                SqlCommand sqlCheckForVariants = new SqlCommand($"SELECT VariantID FROM ProductVariants WHERE ProductID = {productId}", con);
                oreader = sqlCheckForVariants.ExecuteReader();
                while (oreader.Read())
                {
                    variantIdList.Add(int.Parse(oreader["VariantID"].ToString()));
                }
                oreader.Close();
                oreader.Dispose();

                SqlCommand sqlCheckForOriginal = new SqlCommand($"SELECT ProductID FROM ProductVariants WHERE VariantID = {productId}", con);
                bool isNotOriginal = false;
                oreader = sqlCheckForOriginal.ExecuteReader();
                while (oreader.Read())
                {
                    variantIdList.Add(int.Parse(oreader["ProductID"].ToString()));
                    isNotOriginal = true;
                }
                oreader.Close();
                oreader.Dispose();

                if (isNotOriginal)
                {
                    SqlCommand sqlCheckForOriginalVariants = new SqlCommand($"SELECT VariantID FROM ProductVariants WHERE ProductID = {variantIdList[0]}", con);
                    oreader = sqlCheckForOriginalVariants.ExecuteReader();
                    while (oreader.Read())
                    {
                        variantIdList.Add(int.Parse(oreader["ProductID"].ToString()));
                    }
                    sqlCheckForOriginalVariants.Dispose();
                }
                sqlCheckForVariants.Dispose();
                sqlCheckForOriginal.Dispose();
            }
            catch (Exception ex)
            {
                //Error
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
            }
        }

        private void ShowVariants()
        {
            DropDownVariants.Items.Clear();
            ListItem selectedProduct = new ListItem(productTitle.InnerText,productId.ToString());
            DropDownVariants.Items.Add(selectedProduct);
            foreach (int variantId in variantIdList)
            {
                ListItem item = new ListItem(LoadName(variantId), variantId.ToString());
                DropDownVariants.Items.Add(item);
            }
        }

        protected void SendVariantChoice_SelectChange(object sender, EventArgs e)
        {
            Response.Redirect($"Product.aspx?productId={DropDownVariants.SelectedValue}");
        }
    }
}