using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using E_Handel.BL;

namespace E_Handel
{
    public partial class Result : System.Web.UI.Page
    {
        List<BLProduct> resultBLProducts = new List<BLProduct>();

        protected void Page_Load(object sender, EventArgs e)
        {
            String searchString = Request.QueryString["search"];
            if (searchString != null)
            {

                //Validate string
                //Check string with Products.Name
                //Check string with Products.Description
                ResultTitle.InnerHtml = "Search results: ";
            }
            else
            {
                ResultTitle.InnerHtml = "No search results.";
            }
        }
    }
}