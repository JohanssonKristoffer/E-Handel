using E_Handel.BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Handel
{
    public partial class Home : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["KKG-EHandelConnectionString"].ConnectionString;
        List<BLProduct> discountList = new List<BLProduct>();

        protected void Page_Load(object sender, EventArgs e)
        {
            RetrieveDiscountList();
            SelectRandomDiscounts();
        }

        private void SelectRandomDiscounts()
        {
            Random randomProduct = new Random();
            int ad1 = randomProduct.Next(0, discountList.Count-2);
            int ad2 = randomProduct.Next(ad1+1, discountList.Count-1);
          
            ImageAd1.ImageUrl = "ImgHandler.ashx?productId="+ discountList[ad1].Id;
            OriginalPriceAd1.Text = "Original Price: " + "<strike>" + "£" + discountList[ad1].Price + "</strike>";
            DiscountPriceAd1.Text = "Discount Price:" + "£" + (discountList[ad1].Price - (discountList[ad1].Price * discountList[ad1].Discount / 100)).ToString() ; 
            
            ImageAd2.ImageUrl = "ImgHandler.ashx?productId=" + discountList[ad2].Id;
            OriginalPriceAd2.Text = "Original Price: " + "<strike>" + "£" + discountList[ad2].Price.ToString() + "£" + "</strike>";
            DiscountPriceAd2.Text = "Discount Price:" + "£" + (discountList[ad2].Price - (discountList[ad2].Price * discountList[ad2].Discount / 100)).ToString(); 
        }

        private void RetrieveDiscountList()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlGetDiscountedProducts = new SqlCommand($"SELECT ProductID FROM DiscountedProducts", sqlConnection);
            SqlDataReader sqlReader = null;
            try
            {
                sqlConnection.Open();
                sqlReader = sqlGetDiscountedProducts.ExecuteReader();
                while (sqlReader.Read())
                {
                    discountList.Add(new BLProduct(connectionString, int.Parse(sqlReader["ProductID"].ToString())));
                }
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                    sqlReader.Dispose();
                }
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlGetDiscountedProducts.Dispose();
            }
        }
    }
}