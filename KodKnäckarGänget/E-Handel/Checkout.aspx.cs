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
using E_Handel.BL;

namespace E_Handel
{

    public partial class Checkout : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["KKG-EHandelConnectionString"].ConnectionString;
        private List<BLCartProduct> cartList;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCartList();
            InsertProductsIntoTable();
        }

        private void InsertCustomerInformation()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO Orders (Postage,TotalPrice,Address,PostalCode,City,Country,Email,Telephone,Name,Surname)OUTPUT INSERTED.ID VALUES                     ('{tableShippingPrice}','{tableTotalPrice}','{customer_address.Text}','{customer_postalcode.Text}','{customer_city.Text}','{customer_country.Text}','{customer_email.Text}','{customer_phone.Text}','{customer_name.Text}','{customer_surname.Text}')", connection);
                connection.Open();

                Session["orderId"] = (int) cmd.ExecuteScalar();
            }
        }
        
        private void InsertProductsIntoTable()
        {
            foreach (var cartProduct in cartList)
            {
                BLProduct product = new BLProduct(connectionString, cartProduct.Id);

                Image image = new Image();
                image.ImageUrl = $"ImgHandler.ashx?productIdThumb={product.Id}";
                TableCell cellImage = new TableCell();
                cellImage.Controls.Add(image);
                
                Label nameLabel = new Label();
                nameLabel.Text = product.Name;
                TableCell cellName = new TableCell();
                cellName.Controls.Add(nameLabel);


                TableRow row = new TableRow();
                row.Controls.Add(cellImage);
                row.Controls.Add(cellName);

                Checkout_table.Controls.Add(row);


            }

        }

        private void LoadCartList()
        {
            if(Session["cartList"] != null)
                cartList = (List<BLCartProduct>)Session["cartList"];
            else
            {
                //error
            }
        }

        protected void customer_submit_order_Click(object sender, EventArgs e)
        {
            InsertCustomerInformation();
            InsertOrderProducts();
            Response.Redirect("Receipt.aspx");
        }

        private void InsertOrderProducts()
        {
            SqlConnection con = new SqlConnection(connectionString);

            int orderId = (int) Session["orderId"];
            try
            {
                con.Open();
                foreach (BLCartProduct product in cartList)
                {
                    SqlCommand insertProductInOrderProducts = new SqlCommand($"INSERT INTO OrderProducts (OrderID,ProductID,Quantity) VALUES ('{orderId}','{product.Id}', '{product.Quantity}')", con);
                    if (insertProductInOrderProducts.ExecuteNonQuery() < 1)
                    {
                        //error
                    }
                    insertProductInOrderProducts.Dispose();
                }

            }
            catch (Exception ex)
            {
                //error
            }
            finally
            {
                con.Close();
                con.Dispose();
            }




        }
    }
}