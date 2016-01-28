using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;
using E_Handel.BL;

namespace E_Handel
{

    public partial class Product : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["KKG-EHandelConnectionString"].ConnectionString;
        private BLProduct product;

        protected void Page_Load(object sender, EventArgs e)
        {
            int productId;
            if (int.TryParse(Request.QueryString["productId"], out productId))
            {
                LoadProduct(productId);
                if (!Page.IsPostBack)
                    ShowVariants();
            }
            else
                throw new HttpException(404, $"Product.aspx?productId={Request.QueryString["productId"]} doesn't exist.");
        }

        private void LoadProduct(int productId)
        {
            product = BLProduct.RetrieveFromDB(connectionString, productId);
            if (product == null)
                throw new HttpException(404, $"Product.aspx?productId={productId} doesn't exist.");
            productImage.Src = $"ImgHandler.ashx?productId={productId}";
            productTitle.InnerText = product.Name;
            if (product.Discount > 0)
            {
                originalProductPrice.InnerText = "£" + product.Price;
                originalProductPrice.Visible = true;
                double newPrice = product.Price * (1 - product.Discount / 100);
                productPrice.Visible = false;
                discountProductPrice.Visible = true;
                discountProductPrice.InnerText = "£" + newPrice;
            }
            else
                productPrice.InnerText = "£" + product.Price;
            productDescription.InnerText = product.Description;
            if (product.TrailerUrl != null)
            {
                linkViewTrailer.Visible = true;
                linkViewTrailer.HRef = product.TrailerUrl;
            }
        }

        private void ShowVariants()
        {
            List<BLProduct> variantList = BLProduct.RetrieveVariantsOfProductFromDB(connectionString, product.Id);
            DropDownVariants.Items.Clear();
            ListItem selectedProduct = new ListItem(productTitle.InnerText, product.Id.ToString());
            DropDownVariants.Items.Add(selectedProduct);
            foreach (BLProduct variant in variantList)
            {
                ListItem item = new ListItem(variant.Name, variant.Id.ToString());
                DropDownVariants.Items.Add(item);
            }
        }

        protected void SendVariantChoice_SelectChange(object sender, EventArgs e)
        {
            Response.Redirect($"/Product.aspx?productId={DropDownVariants.SelectedValue}");
        }

        protected void AddToCart_Click(object sender, EventArgs e)
        {
            int quantityAdded = int.Parse(productQuantity.Value);

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

            Response.Redirect($"/Product.aspx?productId={product.Id}");
        }
    }
}