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
        List<BLDiscountProduct> discountList = new List<BLDiscountProduct>();

        protected void Page_Load(object sender, EventArgs e)
        {
            RetrieveDiscountList();
            SelectRandomDiscounts();
        }

        private void SelectRandomDiscounts()
        {
            Random randomProduct = new Random();
            int random1 = randomProduct.Next(0, discountList.Count-2);
            int random2 = randomProduct.Next(random1+1, discountList.Count-1);




            BLProduct productAd1 = new BLProduct(connectionString, discountList[random1].Id);
            ImageAd1.ImageUrl = "ImgHandler.ashx?productId="+ productAd1.Id;
            OriginalPriceAd1.Text = "Original Price: " + "<strike>" + "£" + productAd1.Price.ToString() + "</strike>";
            DiscountPriceAd1.Text = "Discount Price:" + "£" + (productAd1.Price - (productAd1.Price * discountList[random1].Discount / 100)).ToString() ; 

            BLProduct productAd2 = new BLProduct(connectionString, discountList[random2].Id);
            ImageAd2.ImageUrl = "ImgHandler.ashx?productId=" + productAd2.Id;
            OriginalPriceAd2.Text = "Original Price: " + "<strike>" + "£" + productAd2.Price.ToString() + "£" + "</strike>";
            DiscountPriceAd2.Text = "Discount Price:" + "£" + (productAd2.Price - (productAd2.Price * discountList[random2].Discount / 100)).ToString(); 
            //DiscountPriceAd1.Text = discountList[random1].Discount.ToString() + "%";

            //BLProduct productAd2 = new BLProduct(connectionString, discountList[ad2Index].Id);
            //ImageAd2.ImageUrl = "ImgHandler.ashx?productId=" + productAd2.Id;
            //DiscountPriceAd1.Text = (productAd2.Price - (productAd2.Price * discountList[ad2Index].Discount / 100)).ToString();
            //

        }

        private void RetrieveDiscountList()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlGetDiscountedProducts = new SqlCommand($"SELECT ProductID, DiscountPercentage FROM DiscountedProducts", sqlConnection);
            SqlDataReader sqlReader = null;
            try
            {
                sqlConnection.Open();
                sqlReader = sqlGetDiscountedProducts.ExecuteReader();
                while (sqlReader.Read())
                {
                    discountList.Add(new BLDiscountProduct(int.Parse(sqlReader["ProductID"].ToString()), double.Parse(sqlReader["DiscountPercentage"].ToString())));
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

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

       
    }
}