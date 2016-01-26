
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
        private List<BLProduct> adProductList = new List<BLProduct>();
        private List<BLProduct> popularProductList = new List<BLProduct>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack || Session["adProductList"] == null)
                RetrieveRandomDiscountedProducts();
            else
                adProductList = (List<BLProduct>)Session["adProductList"];
            ShowAds();
            RetrievePopularProducts();
            ShowPopularProducts();
        }

        private void RetrievePopularProducts()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlGetPopularProducts = new SqlCommand("SELECT TOP 3 ID, CategoryID, Name, Description, Price, Popularity, StockQuantity, VAT FROM Products ORDER BY Popularity DESC", sqlConnection);
            SqlDataReader sqlReader = null;
            try
            {
                sqlConnection.Open();
                sqlReader = sqlGetPopularProducts.ExecuteReader();
                while (sqlReader.Read())
                {
                    int id = int.Parse(sqlReader["ID"].ToString());
                    int categoryId = int.Parse(sqlReader["CategoryID"].ToString());
                    string name = sqlReader["Name"].ToString();
                    string description = sqlReader["Description"].ToString();
                    double price = double.Parse(sqlReader["Price"].ToString());
                    int popularity = int.Parse(sqlReader["Popularity"].ToString());
                    int stockQuantity = int.Parse(sqlReader["StockQuantity"].ToString());
                    double VAT = double.Parse(sqlReader["VAT"].ToString());
                    BLProduct product = new BLProduct(id, categoryId, name, description, price, popularity, stockQuantity, VAT);
                    product.GetDiscountFromDB(connectionString);
                    popularProductList.Add(product);
                }
            }
            catch (Exception)
            {
                throw; //Error retrieving popular products from Products
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
                sqlGetPopularProducts.Dispose();
            }
        }

        private void ShowPopularProducts()
        {
            LinkPop1.NavigateUrl = "Product.aspx?productId=" + popularProductList[0].Id;
            ImagePop1.ImageUrl = "ImgHandler.ashx?productId=" + popularProductList[0].Id;
            TitlePop1.Text = popularProductList[0].Name;
            if (popularProductList[0].Discount > 0)
            {
                OriginalPricePop1.Text = "<strike>£" + popularProductList[0].Price +
                                         "</strike>";
                DiscountPricePop1.Text = "£" +
                                         (popularProductList[0].Price * (1 - popularProductList[0].Discount / 100));
            }
            else
                OriginalPricePop1.Text = "Price: £" + popularProductList[0].Price;

            LinkPop2.NavigateUrl = "Product.aspx?productId=" + popularProductList[1].Id;
            ImagePop2.ImageUrl = "ImgHandler.ashx?productId=" + popularProductList[1].Id;
            TitlePop2.Text = popularProductList[1].Name;
            if (popularProductList[1].Discount > 0)
            {
                OriginalPricePop2.Text = "<strike>£" + popularProductList[1].Price +
                                         "</strike>";
                DiscountPricePop2.Text = "£" +
                                         (popularProductList[1].Price * (1 - popularProductList[1].Discount / 100));
            }
            else
                OriginalPricePop2.Text = "Price: £" + popularProductList[1].Price;

            LinkPop3.NavigateUrl = "Product.aspx?productId=" + popularProductList[2].Id;
            ImagePop3.ImageUrl = "ImgHandler.ashx?productId=" + popularProductList[2].Id;
            TitlePop3.Text = popularProductList[2].Name;
            if (popularProductList[2].Discount > 0)
            {
                OriginalPricePop3.Text = "<strike>£" + popularProductList[2].Price +
                                         "</strike>";
                DiscountPricePop3.Text = "£" +
                                         (popularProductList[2].Price * (1 - popularProductList[2].Discount / 100));
            }
            else
                OriginalPricePop3.Text = "Price: £" + popularProductList[2].Price;
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

                Random randomProduct = new Random();
                int ad1 = randomProduct.Next(0, discountIdList.Count - 2);
                int ad2 = randomProduct.Next(ad1 + 1, discountIdList.Count - 1);
                adProductList.Add(new BLProduct(connectionString, discountIdList[ad1]));
                adProductList.Add(new BLProduct(connectionString, discountIdList[ad2]));
                Session["adProductList"] = adProductList;
            }
            catch (Exception)
            {
                throw; //Error retrieving discounted products from DiscountedProducts or Products
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

        private void ShowAds()
        {
            LinkAd1.NavigateUrl = "Product.aspx?productId=" + adProductList[0].Id;
            ImageAd1.ImageUrl = "ImgHandler.ashx?productId=" + adProductList[0].Id;
            TitleAd1.Text = adProductList[0].Name;
            OriginalPriceAd1.Text = "<strike>£" + adProductList[0].Price + "</strike>";
            DiscountPriceAd1.Text = "£" + (adProductList[0].Price - (adProductList[0].Price * adProductList[0].Discount / 100));

            LinkAd2.NavigateUrl = "Product.aspx?productId=" + adProductList[1].Id;
            ImageAd2.ImageUrl = "ImgHandler.ashx?productId=" + adProductList[1].Id;
            TitleAd2.Text = adProductList[1].Name;
            OriginalPriceAd2.Text = "<strike>£" + adProductList[1].Price + "</strike>";
            DiscountPriceAd2.Text = "£" + (adProductList[1].Price - (adProductList[1].Price * adProductList[1].Discount / 100));
        }

        protected void AddToCartAd1_Click(object sender, EventArgs e) => AddToCart(adProductList[0]);
        protected void AddToCartAd2_Click(object sender, EventArgs e) => AddToCart(adProductList[1]);
        protected void AddtoCartPop1_Click(object sender, EventArgs e) => AddToCart(popularProductList[0]);
        protected void AddtoCartPop2_Click(object sender, EventArgs e) => AddToCart(popularProductList[1]);
        protected void AddtoCartPop3_Click(object sender, EventArgs e) => AddToCart(popularProductList[2]);

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