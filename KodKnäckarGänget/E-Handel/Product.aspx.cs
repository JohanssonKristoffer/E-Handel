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

    public partial class Product : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["KKG-EHandelConnectionString"].ConnectionString;
        private BLProduct product;
        private List<int> variantIdList = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            int productId;
            if (int.TryParse(Request.QueryString["productId"], out productId))
            {
                LoadProduct(productId);
                LoadVariant();
                if (!Page.IsPostBack)
                    ShowVariants();
            }
            else
            {
                productTitle.InnerText = "Unable to retrieve product."; //Error parsing productId from url
            }
        }

        private void LoadProduct(int productId)
        {
            try
            {
                product = new BLProduct(connectionString, productId);
                productImage.Src = $"ImgHandler.ashx?productId={productId}";
                productTitle.InnerText = product.Name;
                if (product.Discount > 0)
                {
                    originalProductPrice.InnerText = "£" + product.Price;
                    originalProductPrice.Visible = true;
                    double newPrice = product.Price * (1 - product.Discount / 100);
                    productPrice.InnerText = "£" + newPrice;
                }
                else
                    productPrice.InnerText = "£" + product.Price;
                productDescription.InnerText = product.Description;
            }
            catch (Exception)
            {
                throw; //Error loading product from Products
            }
        }

        private void LoadVariant()
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataReader oreader = null;
            try
            {
                con.Open();

                SqlCommand sqlCheckForVariants = new SqlCommand($"SELECT VariantID FROM ProductVariants WHERE ProductID = {product.Id}", con);
                oreader = sqlCheckForVariants.ExecuteReader();
                while (oreader.Read())
                {
                    variantIdList.Add(int.Parse(oreader["VariantID"].ToString()));
                }
                oreader.Close();
                oreader.Dispose();

                SqlCommand sqlCheckForOriginal = new SqlCommand($"SELECT ProductID FROM ProductVariants WHERE VariantID = {product.Id}", con);
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
                        if (int.Parse(oreader["VariantID"].ToString()) != product.Id)
                            variantIdList.Add(int.Parse(oreader["VariantID"].ToString()));
                    }
                    sqlCheckForOriginalVariants.Dispose();
                }
                sqlCheckForVariants.Dispose();
                sqlCheckForOriginal.Dispose();
            }
            catch (Exception)
            {
                throw; //Error loading variants from ProductVariants
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
            try
            {
                DropDownVariants.Items.Clear();
                ListItem selectedProduct = new ListItem(productTitle.InnerText, product.Id.ToString());
                DropDownVariants.Items.Add(selectedProduct);
                foreach (int variantId in variantIdList)
                {
                    BLProduct variant = new BLProduct(connectionString, variantId);
                    ListItem item = new ListItem(variant.Name, variantId.ToString());
                    DropDownVariants.Items.Add(item);
                }
            }
            catch (Exception)
            {
                throw; //Error loading variant from Products
            }
        }

        protected void SendVariantChoice_SelectChange(object sender, EventArgs e)
        {
            Response.Redirect($"Product.aspx?productId={DropDownVariants.SelectedValue}");
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

            Response.Redirect($"Product.aspx?productId={product.Id}");
        }
    }
}