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
        int productId;
        List<int> variantIdList = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["productId"], out productId))
            {
                LoadProduct();
                LoadVariant();
                if (!Page.IsPostBack)
                    ShowVariants();
            }
            else
            {
                productTitle.InnerText = "Unable to retrieve product.";//error
            }
        }

        private void LoadProduct()
        {
            BLProduct product = new BLProduct(connectionString, productId);
            productImage.Src = $"ImgHandler.ashx?productId={productId}";
            productTitle.InnerText = product.Name;
            productPrice.InnerText = "£" + product.Price;
            productDescription.InnerText = product.Description;
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
                        if (int.Parse(oreader["VariantID"].ToString()) != productId)
                            variantIdList.Add(int.Parse(oreader["VariantID"].ToString()));
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
            ListItem selectedProduct = new ListItem(productTitle.InnerText, productId.ToString());
            DropDownVariants.Items.Add(selectedProduct);
            foreach (int variantId in variantIdList)
            {
                BLProduct variant = new BLProduct(connectionString, variantId);
                ListItem item = new ListItem(variant.Name, variantId.ToString());
                DropDownVariants.Items.Add(item);
            }
        }

        protected void SendVariantChoice_SelectChange(object sender, EventArgs e)
        {
            Response.Redirect($"Product.aspx?productId={DropDownVariants.SelectedValue}");
        }

        protected void AddToCart_Click(object sender, EventArgs e)
        {
            int cartCount = 0;
            if (Session["cartCount"] != null)
                cartCount = (int)Session["cartCount"];
            cartCount += int.Parse(productQuantity.Value);

            List<BLCartProduct> cartList;
            if (Session["cartList"] != null)
                cartList = (List<BLCartProduct>)Session["cartList"];
            else
                cartList = new List<BLCartProduct>();

            bool alreadyInCart = false;
            foreach (BLCartProduct cartProduct in cartList)
            {
                if (cartProduct.Id == productId)
                {
                    alreadyInCart = true;
                    cartProduct.Quantity += int.Parse(productQuantity.Value);
                    break;
                }
            }
            if (!alreadyInCart)
                cartList.Add(new BLCartProduct(id: productId, quantity: int.Parse(productQuantity.Value)));

            Session["cartCount"] = cartCount;
            Session["cartList"] = cartList;

            Response.Redirect($"Product.aspx?productId={productId}");
        }
    }
}