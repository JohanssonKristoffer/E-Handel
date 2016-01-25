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
        private BLProduct adProduct1;
        private BLProduct adProduct2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                RetrieveRandomDiscountedProducts();
            else
            {
                adProduct1 = (BLProduct)Session["adProduct1"];
                adProduct2 = (BLProduct)Session["adProduct2"];
            }
            ShowAds();
        }

        private void RetrieveRandomDiscountedProducts()
        {
            List<int> discountIdList = new List<int>();

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlGetDiscountedProducts = new SqlCommand("SELECT ProductID FROM DiscountedProducts", sqlConnection);
            SqlDataReader sqlReader = null;
            try
            {
                sqlConnection.Open();
                sqlReader = sqlGetDiscountedProducts.ExecuteReader();
                while (sqlReader.Read())
                {
                    discountIdList.Add(int.Parse(sqlReader["ProductID"].ToString()));
                }
            }
            catch (Exception)
            {
                //Error
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

            Random randomProduct = new Random();
            int ad1 = randomProduct.Next(0, discountIdList.Count - 2);
            int ad2 = randomProduct.Next(ad1 + 1, discountIdList.Count - 1);
            adProduct1 = new BLProduct(connectionString, discountIdList[ad1]);
            adProduct2 = new BLProduct(connectionString, discountIdList[ad2]);
            Session["adProduct1"] = adProduct1;
            Session["adProduct2"] = adProduct2;
        }

        private void ShowAds()
        {
            LinkAd1.NavigateUrl = "Product.aspx?productId=" + adProduct1.Id;
            ImageAd1.ImageUrl = "ImgHandler.ashx?productId=" + adProduct1.Id;
            OriginalPriceAd1.Text = "Original Price: " + "<strike>" + "£" + adProduct1.Price + "</strike>";
            DiscountPriceAd1.Text = "Discount Price:" + "£" + (adProduct1.Price - (adProduct1.Price * adProduct1.Discount / 100));

            LinkAd2.NavigateUrl = "Product.aspx?productId=" + adProduct2.Id;
            ImageAd2.ImageUrl = "ImgHandler.ashx?productId=" + adProduct2.Id;
            OriginalPriceAd2.Text = "Original Price: " + "<strike>" + "£" + adProduct2.Price + "£" + "</strike>";
            DiscountPriceAd2.Text = "Discount Price:" + "£" + (adProduct2.Price - (adProduct2.Price * adProduct2.Discount / 100));
        }

        protected void AddToCartAd1_Click(object sender, EventArgs e)
        {
            AddToCart(adProduct1);
        }

        protected void AddToCartAd2_Click(object sender, EventArgs e)
        {
            AddToCart(adProduct2);
        }

        private void AddToCart(BLProduct product)
        {
            int quantityAdded = 1;

            List<BLCartProduct> cartList;
            if (Session["cartList"] != null)
                cartList = (List<BLCartProduct>)Session["cartList"];
            else
                cartList = new List<BLCartProduct>();

            bool alreadyInCart = false;
            foreach (BLCartProduct cartProduct in cartList)
            {
                if (cartProduct.Id == product.Id)
                {
                    alreadyInCart = true;
                    if ((quantityAdded + cartProduct.Quantity) > product.StockQuantity)
                        quantityAdded = product.StockQuantity - cartProduct.Quantity;
                    cartProduct.Quantity += quantityAdded;
                    break;
                }
            }
            if (!alreadyInCart)
                cartList.Add(new BLCartProduct(id: product.Id, quantity: quantityAdded));

            int cartCount = 0;
            if (Session["cartCount"] != null)
                cartCount = (int)Session["cartCount"];
            cartCount += quantityAdded;

            Session["cartCount"] = cartCount;
            Session["cartList"] = cartList;

            Response.Redirect($"Home.aspx");
        }
    }
}