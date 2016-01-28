using E_Handel.BL;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            ShowPopularProducts();
            PopulateCarousel();
        }

        private void PopulateCarousel()
        {
            List<BLCategory> categories = BLCategory.RetrieveListFromDB(connectionString);
            foreach (BLCategory category in categories)
            {
                Image image = new Image
                {
                    CssClass = "carousel-image",
                    ImageUrl = $"ImgHandler.ashx?categoryId={category.Id}"
                };
                HyperLink link = new HyperLink
                {
                    NavigateUrl = $"/Result.aspx?categoryId={category.Id}"
                };
                link.Controls.Add(image);

                Label captionLabel = new Label
                {
                    Text = $"<h3 style = 'color: #FFFFFF' > {category.Name} </ h3 >"
                };
                Panel captionPanel = new Panel { CssClass = "carousel-caption" };
                captionPanel.Controls.Add(captionLabel);

                Panel itemPanel = new Panel { CssClass = "item" };
                itemPanel.Controls.Add(link);
                itemPanel.Controls.Add(captionPanel);

                categoryCarousel.Controls.Add(itemPanel);
            }
        }

        private void ShowPopularProducts()
        {
            popularProductList = BLProduct.RetrieveTopNPopularProductsFromDB(connectionString, numberOfProducts: 3);

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
            List<BLProduct> discountedProducts = BLProduct.RetrieveDiscountedProductsFromDB(connectionString);

            Random randomProduct = new Random();
            int ad1 = randomProduct.Next(0, discountedProducts.Count - 2);
            int ad2 = randomProduct.Next(ad1 + 1, discountedProducts.Count - 1);
            adProductList.Add(discountedProducts[ad1]);
            adProductList.Add(discountedProducts[ad2]);
            Session["adProductList"] = adProductList;
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

            Response.Redirect("Home.aspx");
        }
    }
}